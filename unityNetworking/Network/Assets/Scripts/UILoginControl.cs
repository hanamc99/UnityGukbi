using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using Newtonsoft.Json;

public class UILoginControl : MonoBehaviour
{
    public Button btnLogIn;
    public Button btnRegister;
    public InputField inputEmail;
    public InputField inputPassword;
    public UnityEvent<string> onLogInResult = new UnityEvent<string>();
    
    void Start()
    {
        this.btnLogIn.onClick.AddListener(() =>
        {
            StartCoroutine(WaitForPostLogIn(inputEmail.text, inputPassword.text));
        });
    }

    public void OnWrongPwd()
    {
        this.inputPassword.text = "";
    }

    public void EraseInputField()
    {
        this.inputEmail.text = "";
        this.inputPassword.text = "";
    }

    IEnumerator WaitForPostLogIn(string email, string pwd)
    {
        //��� ��ü�� ���� 
        UnityWebRequest www = new UnityWebRequest("http://localhost:3000/login", "POST");

        //������ ��ü�� ���� 
        Packets.req_login login = new Packets.req_login(email, pwd);

        //��ü�� ������ȭ 
        string json = JsonConvert.SerializeObject(login);

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
            this.onLogInResult.Invoke(www.downloadHandler.text);
        }
    }
}
