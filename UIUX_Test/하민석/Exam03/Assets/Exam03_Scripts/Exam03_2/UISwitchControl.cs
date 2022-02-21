//이름: 하민석
//과목: 게임 UI & UX 프로그래밍
//날짜: 2022-02-21​

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitchControl : MonoBehaviour
{
    [SerializeField] Button[] btn;

    void Start()
    {
        btn[0].onClick.AddListener(() =>
        {
            btn[0].gameObject.SetActive(false);
            btn[1].gameObject.SetActive(true);
            Debug.Log("ON");
        });
        btn[1].onClick.AddListener(() =>
        {
            btn[1].gameObject.SetActive(false);
            btn[0].gameObject.SetActive(true);
            Debug.Log("OFF");
        });
    }
}
