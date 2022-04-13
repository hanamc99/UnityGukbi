using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APP : MonoBehaviour
{
    public Text versionText;
    public Button incAchiveBtn;
    public Button signBtn;

    void Start()
    {
        versionText.text = Application.version;
        Debug.Log("Hello hmsGPGS 2022");
        GPGSManager.instance.Init();
        //GPGSManager.instance.Authenticate();
        signBtn.onClick.AddListener(() => {
            GPGSManager.instance.Authenticate();
        });
        /*incAchiveBtn.onClick.AddListener(() => {
            GPGSManager.instance.IncrementAchivement();
        });*/
    }
}
