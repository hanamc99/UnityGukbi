//이름: 하민석
//과목: 게임 UI & UX 프로그래밍
//날짜: 2022-02-21​

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exam03_4_Main : MonoBehaviour
{
    void Awake()
    {
        InfoAndDataManager.GetInstance().LoadStageInfo();
    }
}
