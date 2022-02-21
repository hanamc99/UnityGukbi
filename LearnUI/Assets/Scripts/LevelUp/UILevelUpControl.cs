using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILevelUpControl : MonoBehaviour
{
    [SerializeField] Button restart;
    [SerializeField] GameObject goAnim;

    void Start()
    {
        
    }

    public void RestartAnim()
    {
        if (goAnim.activeSelf)
        {
            goAnim.SetActive(false);
        }
        else
        {
            goAnim.SetActive(true);
        }
    }
}
