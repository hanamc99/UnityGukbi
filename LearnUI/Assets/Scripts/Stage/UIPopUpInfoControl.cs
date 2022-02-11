using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUpInfoControl : MonoBehaviour
{
    public Text stageNameText;
    public Text missionNameText;
    public Image[] availItemImage = new Image[3];
    public GameObject[] availItemAmountText = new GameObject[3];
    [SerializeField] GameObject[] itemCheckIcon = new GameObject[3];
    [SerializeField] Button btnStart;
    [HideInInspector] public int[] itemIds = new int[3];
    List<int> selectedItemIds = new List<int>();

    void Start()
    {
        InitImgBtn();
        InitStartBtn();
    }

    void InitStartBtn()
    {
        btnStart.onClick.AddListener(GetStarted);
    }

    void GetStarted()
    {
        for(int t = 0; t < selectedItemIds.Count; t++)
        {
            DataManager.GetInstance().UseItem(selectedItemIds[t]);
        }
        ResetUISettings();
    }

    void InitImgBtn()
    {
        for (int i = 0; i < availItemImage.Length; i++)
        {
            GiveIndexToImgBtn(i);
        }
    }

    void GiveIndexToImgBtn(int index)
    {
        availItemImage[index].GetComponent<Button>().onClick.AddListener(() =>
        {
            if (availItemAmountText[index].gameObject.activeSelf)
            {
                if(DataManager.GetInstance().GetItemInfoAmount(itemIds[index]) > 0)
                {
                    selectedItemIds.Add(itemIds[index]);
                    itemCheckIcon[index].gameObject.SetActive(true);
                    availItemAmountText[index].gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("해당 아이템을 갖고 있지 않습니다.");
                }
            }
            else
            {
                selectedItemIds.Remove(itemIds[index]);
                itemCheckIcon[index].gameObject.SetActive(false);
                availItemAmountText[index].gameObject.SetActive(true);
            }
            foreach(int num in selectedItemIds)
            {
                Debug.Log("선택된 아이템의 id는 " + num);
            }
        }
        );
    }

    public void ClosePopUpInfo()
    {
        ResetUISettings();
        gameObject.SetActive(false);
    }

    void ResetUISettings()
    {
        for (int k = 0; k < itemCheckIcon.Length; k++)
        {
            itemCheckIcon[k].SetActive(false);
            availItemAmountText[k].SetActive(true);
        }
        selectedItemIds.RemoveRange(0, selectedItemIds.Count);

        for(int n = 0; n < itemIds.Length; n++)
        {
            if(itemIds[n] != 0)
            {
                availItemAmountText[n].GetComponentInChildren<Text>().text = DataManager.GetInstance().GetItemInfoAmount(itemIds[n]) + "";
            }
        }
    }
}
