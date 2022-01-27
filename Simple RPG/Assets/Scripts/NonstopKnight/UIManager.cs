using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text text1;
    [SerializeField] Text text2;
    [SerializeField] Text floorText;

    void Start()
    {
        
    }

    void Update()
    {
        floorText.text = DataManage.instance.gi.floor + "층";
        text1.text = DataManage.instance.gi.weapon.name;
        text2.text = "무기 공격력 : " + DataManage.instance.gi.weapon.damage;
    }
}
