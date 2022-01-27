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
        floorText.text = DataManage.instance.gi.floor + "��";
        text1.text = DataManage.instance.gi.weapon.name;
        text2.text = "���� ���ݷ� : " + DataManage.instance.gi.weapon.damage;
    }
}
