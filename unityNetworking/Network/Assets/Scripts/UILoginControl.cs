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
        //통신 객체를 생성 
        UnityWebRequest www = new UnityWebRequest("http://localhost:3000/login", "POST");

        //데이터 객체를 생성 
        Packets.req_login login = new Packets.req_login(email, pwd);

        //객체를 역직렬화 
        string json = JsonConvert.SerializeObject(login);

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

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("네트워크 환경이 좋지 않습니다.");
        }
        else
        {
            this.onLogInResult.Invoke(www.downloadHandler.text);
        }
    }
}
