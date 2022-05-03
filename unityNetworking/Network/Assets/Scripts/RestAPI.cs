using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Packets
{
    public class req_logout
    {
        public string email;
        public req_logout(string email)
        {
            this.email = email;
        }
    }

    public class req_signup
    {
        public string email;
        public string password;
        public string name;
        public req_signup(string email, string password, string name)
        {
            this.email = email;
            this.password = password;
            this.name = name;
        }
    }

    public class req_login
    {
        public string email;
        public string password;
        public req_login(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }

    public class result
    {
        public int status;
        public string message;
    }

    public class res_login : result
    {
        public string email;
        public string name;
    }
}

public enum StatCode
{
    OK = 200,
    NOT_MEM = 500,
    WRONG_PWD = 501,
    EXIST_MEM = 600,
}

public class RestAPI : MonoBehaviour
{
    public UILoginControl uiLogIn;
    public UISignUpControl uiSignUp;
    public UILoggedControl uiLogged;
    public Text titleText;
    public Text statText;

    void Start()
    {
        uiSignUp.gameObject.SetActive(false);
        uiLogged.gameObject.SetActive(false);
        uiLogIn.gameObject.SetActive(true);
        this.titleText.text = "�α���";
        this.statText.text = "";

        uiSignUp.onSignUpResult.AddListener((res_signup) =>
        {
            Packets.result result = JsonConvert.DeserializeObject<Packets.result>(res_signup);
            if((StatCode)result.status == StatCode.OK)
            {
                uiSignUp.EraseInputField();
                uiSignUp.gameObject.SetActive(false);
                uiLogIn.gameObject.SetActive(true);
                this.titleText.text = "�α���";
                this.statText.text = "";
            }
            else if((StatCode)result.status == StatCode.EXIST_MEM)
            {
            }
            this.statText.text = result.message;
        });

        uiSignUp.btnBack.onClick.AddListener(() =>
        {
            uiSignUp.EraseInputField();
            uiSignUp.gameObject.SetActive(false);
            uiLogIn.gameObject.SetActive(true);
            this.titleText.text = "�α���";
            this.statText.text = "";
        });

        uiLogIn.onLogInResult.AddListener((res_login) =>
        {
            Packets.res_login result = JsonConvert.DeserializeObject<Packets.res_login>(res_login);
            if((StatCode)result.status == StatCode.NOT_MEM)
            {
                this.statText.text = result.message;
            }
            else if((StatCode)result.status == StatCode.OK)
            {
                uiLogIn.EraseInputField();
                uiLogIn.gameObject.SetActive(false);
                uiLogged.gameObject.SetActive(true);
                this.titleText.text = "Logged";
                this.statText.text = result.name + "��, " + result.message;
                uiLogged.SetUserInfo(result.email, result.name);
            }
            else if((StatCode)result.status == StatCode.WRONG_PWD)
            {
                uiLogIn.OnWrongPwd();
                this.statText.text = result.message;
            }
        });

        uiLogIn.btnRegister.onClick.AddListener(() =>
        {
            uiLogIn.EraseInputField();
            uiLogIn.gameObject.SetActive(false);
            uiSignUp.gameObject.SetActive(true);
            this.titleText.text = "ȸ�� ����";
            this.statText.text = "";
        });

        uiLogged.onLogOutResult.AddListener((res_logout) =>
        {
            uiLogged.gameObject.SetActive(false);
            uiLogIn.gameObject.SetActive(true);
            this.titleText.text = "�α���";
            Packets.result result = JsonConvert.DeserializeObject<Packets.result>(res_logout);
            this.statText.text = result.message;
        });
    }

    //IEnumerator WaitForGet()
    //{
    //    //UnityWebRequest��ü�� ����� 
    //    UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/members");
    //    //��û�� ������ 
    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.ConnectionError)
    //    {
    //        Debug.Log("��Ʈ��ũ ȯ���� ���� �ʽ��ϴ�.");
    //    }
    //    else
    //    {
    //        Debug.Log(www.downloadHandler.text);
    //    }
    //}
}