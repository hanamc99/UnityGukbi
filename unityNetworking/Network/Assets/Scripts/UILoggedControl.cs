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

        //통신 객체를 생성 
        UnityWebRequest www = new UnityWebRequest("http://localhost:3000/logout", "POST");

        //데이터 객체를 생성 
        Packets.req_logout logout = new Packets.req_logout(userEmail);

        //객체를 역직렬화 
        string json = JsonConvert.SerializeObject(logout);

        //문자열을 바이트 배열로 변환 
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        //통신 객체 바디에 정보를 실었다 
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);

        //응답객체를 담은 버퍼 
        www.downloadHandler = new DownloadHandlerBuffer();

        //헤더에 보낼 데이터의 형식을 기록 
        www.SetRequestHeader("Content-Type", "application/json");

        //요청을 보냄 
        yield return www.SendWebRequest();

        this.userEmail = null;
        this.userPassword = null;
        this.userName = null;

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("네트워크 환경이 좋지 않습니다.");
        }
        else
        {
            this.onLogOutResult.Invoke(www.downloadHandler.text);
        }
    }
}
