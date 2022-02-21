//이름: 하민석
//과목: 게임 UI & UX 프로그래밍
//날짜: 2022-02-21​
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputControl : MonoBehaviour
{
    [SerializeField] InputField input;
    [SerializeField] Button btn;

    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            Debug.Log(input.text);
        });
    }
}

