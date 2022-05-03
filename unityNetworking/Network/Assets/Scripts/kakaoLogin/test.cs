using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Button btnKakaoLogin;
    UniWebView webView;
    public GameObject webViewPrefab;

    private void Start()
    {
        this.btnKakaoLogin.onClick.AddListener(() =>
        {
            Debug.Log("kakao login");

            

        });
    }

    public void TestDebugLog()
    {
        Debug.Log("hello");
    }
}
