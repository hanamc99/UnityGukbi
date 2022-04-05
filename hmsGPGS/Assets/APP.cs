using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APP : MonoBehaviour
{
    public Text versionText;

    void Start()
    {
        versionText.text = Application.version;
        Debug.Log("Hello hmsGPGS 2022");
    }
}
