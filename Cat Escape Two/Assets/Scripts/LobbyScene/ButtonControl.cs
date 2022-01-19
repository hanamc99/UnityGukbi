using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private int index;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetIndex);
        GetComponent<Button>().onClick.AddListener(ToGameScene.instance.MoveToGameScene);
    }

    public void SetIndex()
    {
        ToGameScene.instance.SetIndex(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
