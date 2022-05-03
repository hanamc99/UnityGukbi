using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Events;

public class UISignUpControl : MonoBehaviour
{
    public Button btnSignUp;
    public Button btnBack;
    public InputField inputEmail;
    public InputField inputPassword;
    public InputField inputName;
    public UnityEvent<string> onSignUpResult = new UnityEvent<string>();

    void Start()
    {
        this.btnSignUp.onClick.AddListener(() =>
        {
            StartCoroutine(WaitForPostSignUp(inputEmail.text, inputPassword.text, inputName.text));
        });
    }

    public void EraseInputField()
    {
        this.inputEmail.text = "";
        this.inputPassword.text = "";
        this.inputName.text = "";
    }

    IEnumerator WaitForPostSignUp(string email, string pwd, string name)
    {
        //��� ��ü�� ���� 
        UnityWebRequest www = new UnityWebRequest("http://localhost:3000/members", "POST");

        //������ ��ü�� ���� 
        Packets.req_signup signup = new Packets.req_signup(email, pwd, name);

        //��ü�� ������ȭ 
        string json = JsonConvert.SerializeObject(signup);

        //���ڿ��� ����Ʈ �迭�� ��ȯ 
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        //��� ��ü �ٵ� ������ �Ǿ��� 
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);

        //���䰴ü�� ���� ���� 
        www.downloadHandler = new DownloadHandlerBuffer();

        //����� ���� �������� ������ ��� 
        www.SetRequestHeader("Content-Type", "application/json");

        //��û�� ���� 
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� �ʽ��ϴ�.");
        }
        else
        {
            this.onSignUpResult.Invoke(www.downloadHandler.text);
        }
    }
}
