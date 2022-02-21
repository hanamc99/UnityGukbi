using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TestMain : MonoBehaviour
{
    [SerializeField] Button btnAttack;
    [SerializeField] Animator anim1;
    [SerializeField] Animator anim2;

    [SerializeField] GameObject hudText;
    [SerializeField] Transform point;
    [SerializeField] Transform canvasRect;

    void Start()
    {
        anim1.gameObject.GetComponent<AnimationEventReceiver>().animEventTrigger.AddListener((eventName) =>
        {
            Debug.Log(eventName + "made a lot of damage!");
            anim2.Play("GetHit", -1, 0);
            GameObject txt = Instantiate(hudText, canvasRect);
            txt.transform.position = Camera.main.WorldToScreenPoint(point.position);

            txt.transform.DOLocalMoveY(200f, 0.5f).onComplete = () =>
            {
                txt.transform.DOScale(1, 1f).onComplete = () => Destroy(txt);
            };
        });

        btnAttack.onClick.AddListener(() =>
        {
            anim1.Play("Attack01", -1, 0);
        });
    }
}
