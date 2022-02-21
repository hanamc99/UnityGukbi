using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSlotControl : MonoBehaviour
{
    [SerializeField] Text indexText;
    [SerializeField] GameObject[] stars;

    public void InitSlot(int index, int star)
    {
        indexText.text = index + "";
        for(int i = 0; i < star; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
