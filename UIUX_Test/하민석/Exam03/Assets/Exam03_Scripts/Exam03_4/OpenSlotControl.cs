using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSlotControl : MonoBehaviour
{
    [SerializeField] Text indexText;

    public void InitSlot(int index)
    {
        indexText.text = index + "";
    }
}
