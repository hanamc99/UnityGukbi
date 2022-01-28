using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    ItemGenerator ig;
    [SerializeField] Text pointText;
    [SerializeField] Text timerText;
    [HideInInspector] public int point = 0;
    float time = 30f;

    void Start()
    {
        ig = FindObjectOfType<ItemGenerator>();
        ig.SetMembers(0.25f, 0.5f);
    }

    void Update()
    {
        time -= Time.deltaTime;
        this.timerText.text = this.time.ToString("F1");
        this.pointText.text = this.point + " Point";

        if(time <= 15f)
        {
            ig.SetMembers(0.1f, 0.5f);
        }

        if(time <= 0)
        {
            Time.timeScale = 0f;
        }
    }
}
