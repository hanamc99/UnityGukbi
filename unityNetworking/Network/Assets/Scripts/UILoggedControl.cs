using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using Newtonsoft.Json;


public class UILoggedControl : MonoBehaviour
{
    public Button btnLogOut;
    string userEmail;
    string userPassword;
    string userName;
    public UnityEvent<string> onLogOutResult = new UnityEvent<string>();

    void Start()
    {
        this.btnLogOut.onClick.AddListener(() =>
        {
            StartCoroutine(WaitForPostLogOut());
        });
    }

    public void SetUserInfo(string email, string name)
    {
        this.userEmail = email;
        this.userName = name;
    }

    IEnumerator WaitForPostLogOut()
    {

        //��� ��ü�� ���� 
        UnityWebRequest www = new UnityWebRequest("http://localhost:3000/logout", "POST");

        //������ ��ü�� ���� 
        Packets.req_logout logout = new Packets.req_logout(userEmail);

        //��ü�� ������ȭ 
        string json = JsonConvert.SerializeObject(logout);

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

        this.userEmail = null;
        this.userPassword = null;
        this.userName = null;

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� �ʽ��ϴ�.");
        }
        else
        {
            this.onLogOutResult.Invoke(www.downloadHandler.text);
        }
    }
}
