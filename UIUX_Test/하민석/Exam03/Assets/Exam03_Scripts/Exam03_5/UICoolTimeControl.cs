//이름: 하민석
//과목: 게임 UI & UX 프로그래밍
//날짜: 2022-02-21​

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoolTimeControl : MonoBehaviour
{
    [SerializeField] Text coolText;
    [SerializeField] Image coolFill;
    [SerializeField] Button btn;
    float timer;

    void Start()
    {
        btn.onClick.AddListener(() => 
        {
            if(timer <= 0f)
            {
                StartCoolTime();
            }
        });
    }

    void StartCoolTime()
    {
        StartCoroutine(CoolTimeRoutine());
    }

    IEnumerator CoolTimeRoutine()
    {
        timer = 5f;
        coolFill.fillAmount = 0f;
        while (true)
        {
            timer -= Time.deltaTime;
            coolFill.fillAmount = (5f - timer) / 5f;
            if(timer > 1f)
            {
                coolText.text = (int)timer + "";
            } else if(timer > 0f)
            {
                coolText.text = string.Format("{0:0.#}", timer);
            } else if(timer <= 0f)
            {
                coolText.text = "";
                break;
            }
            yield return null;
        }
    }
}
