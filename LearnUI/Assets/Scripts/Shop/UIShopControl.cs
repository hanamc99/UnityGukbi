using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopControl : MonoBehaviour
{
    [SerializeField] GameObject[] goPopUps;
    bool[] isGen = new bool[3];

    void Start()
    {
        isGen[0] = false;
        isGen[1] = false;
        isGen[2] = false;
    }

    public void ClosePopUp()
    {
        foreach (GameObject popUp in goPopUps)
        {
            popUp.SetActive(false);
        }
    }

    public void OpenGoldPopUp()
    {
        foreach(GameObject popUp in goPopUps)
        {
            popUp.SetActive(false);
        }
        if (!isGen[0])
        {
            goPopUps[0].GetComponent<UIShopPopUpInfoControl>().InitPopUp(0);
            isGen[0] = true;
        }
        goPopUps[0].SetActive(true);
    }

    public void OpenGemPopUp()
    {
        foreach (GameObject popUp in goPopUps)
        {
            popUp.SetActive(false);
        }
        if (!isGen[1])
        {
            goPopUps[1].GetComponent<UIShopPopUpInfoControl>().InitPopUp(1);
            isGen[1] = true;
        }
        goPopUps[1].SetActive(true);
    }

    public void OpenSoulGemPopUp()
    {
        foreach (GameObject popUp in goPopUps)
        {
            popUp.SetActive(false);
        }
        if (!isGen[2])
        {
            goPopUps[2].GetComponent<UIShopPopUpInfoControl>().InitPopUp(2);
            isGen[2] = true;
        }
        goPopUps[2].SetActive(true);
    }
}
