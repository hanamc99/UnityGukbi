using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIStageSlotControl : MonoBehaviour
{
    int id;
    bool isLocked = true;
    [SerializeField] Image lockIcon;
    [SerializeField] Text stageNumText;
    int stageNum;
    [SerializeField] GameObject[] goStar = new GameObject[3];
    UIPopUpInfoControl popUpInfo;
    UILockedPopUpControl lockedPopUp;
    Button btnPopUpInfo;
    [SerializeField] SpriteAtlas atlas;
    [SerializeField] Sprite[] slotFrame = new Sprite[2];

    public void InitStageNum(int num)
    {
        this.stageNum = num;
        this.id = num + 99;
        this.stageNumText.text = stageNum + "";
    }

    void OnEnable()
    {
        SlotBtnInit();
    }

    void Start()
    {
        OpenLock();
        MarkStar();
        if (popUpInfo.gameObject.activeSelf)
        {
            popUpInfo.gameObject.SetActive(false);
            lockedPopUp.gameObject.SetActive(false);
        }
    }

    void SlotBtnInit()
    {
        popUpInfo = FindObjectOfType<UIPopUpInfoControl>();
        lockedPopUp = FindObjectOfType<UILockedPopUpControl>();
        btnPopUpInfo = GetComponent<Button>();
        btnPopUpInfo.onClick.AddListener(PopUpInfo);
    }

    void PopUpInfo()
    {
        Dictionary<int, StageData> dict = DataManager.GetInstance().GetDictStageData();
        StageData data = dict[id];
        if (isLocked)
        {
            lockedPopUp.stageNameText.text = data.name;
            lockedPopUp.minLevelText.text = "Required Level : " + data.requireLevel;
            lockedPopUp.gameObject.SetActive(true);
            return;
        }
        popUpInfo.stageNameText.text = data.name;
        popUpInfo.missionNameText.text = DataManager.GetInstance().GetMissionData(data.stage_mission_id).name;
        for(int i = 0; i < 3; i++)
        {
            ItemData itemdata = null;
            switch (i)
            {
                case 0:
                    {
                        itemdata = DataManager.GetInstance().GetItemData(data.availableItem_id_0);
                        break;
                    }
                case 1:
                    {
                        itemdata = DataManager.GetInstance().GetItemData(data.availableItem_id_1);
                        break;
                    }
                case 2:
                    {
                        itemdata = DataManager.GetInstance().GetItemData(data.availableItem_id_2);
                        break;
                    }
            }
            if (itemdata == null)
            {
                popUpInfo.availItemImage[i].gameObject.SetActive(false);
                continue;
            }
            popUpInfo.itemIds[i] = itemdata.id;
            popUpInfo.availItemImage[i].sprite = atlas.GetSprite(itemdata.spriteName);
            popUpInfo.availItemAmountText[i].GetComponentInChildren<Text>().text = DataManager.GetInstance().GetItemInfoAmount(itemdata.id) + "";
            popUpInfo.availItemImage[i].gameObject.SetActive(true);
        }

        popUpInfo.gameObject.SetActive(true);
    }

    public void MarkStar()
    {
        List<StageInfo> list = DataManager.GetInstance().GetListStageInfo();
        foreach (StageInfo info in list)
        {
            if (info.id == id)
            {
                this.gameObject.GetComponent<Image>().sprite = slotFrame[1];

                for(int i = 0; i < info.star; i++)
                {
                    goStar[i].SetActive(true);
                }
                return;
            }
        }
    }

    public void OpenLock()
    {
        if(this.id == 100)
        {
            this.isLocked = false;
            this.lockIcon.gameObject.SetActive(false);
            this.gameObject.GetComponent<Image>().sprite = slotFrame[0];
        }
        else
        {
            List<StageInfo> list = DataManager.GetInstance().GetListStageInfo();
            foreach (StageInfo info in list)
            {
                Dictionary<int, StageData> dict = DataManager.GetInstance().GetDictStageData();
                StageData data = dict[this.id];
                if (data.requireLevel <= DataManager.GetInstance().GetHeroInfo().level)
                {
                    if (info.id == id)
                    {
                        this.isLocked = false;
                        this.lockIcon.gameObject.SetActive(false);
                        this.gameObject.GetComponent<Image>().sprite = slotFrame[1];
                    }
                    else if (info.id + 1 == id)
                    {
                        this.isLocked = false;
                        this.lockIcon.gameObject.SetActive(false);
                        this.gameObject.GetComponent<Image>().sprite = slotFrame[0];
                        break;
                    }
                }
            }
        }
    }
}
