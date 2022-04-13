using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GunControl gun;

    public Transform gunPivot;
    public Transform lefhHand;
    public Transform rightHand;

    public FloatingJoystick joyStick;
    public Button btnFire;
    public Button btnReload;
    Animator anim;
    float moveSpeed = 5f;
    public bool useKey;

    public bool isDead = false;
    float maxHp = 100;
    float hp;
    [SerializeField] Image hpGauge;


    void Start()
    {
        hp = maxHp;
        anim = GetComponent<Animator>();
        //joyStick.SnapX = true;
        //joyStick.SnapY = true;
        btnReload.onClick.AddListener(() => {
            anim.SetTrigger("Reload");
            gun.Reload();
        });
        btnFire.onClick.AddListener(() =>
        {
            gun.Fire();
        });
    }

    void Update()
    {
        if (useKey)
        {
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");
            var dir = new Vector2(h, v);

            if (dir != Vector2.zero)
            {
                float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                this.anim.SetInteger("MoveVec", 1);

                this.transform.Translate(new Vector3(dir.x, 0, dir.y).normalized * this.moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                this.anim.SetInteger("MoveVec", 0);
            }
        }
        else
        {
            if (joyStick.Direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(joyStick.Direction.x, joyStick.Direction.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                anim.SetInteger("MoveVec", 1);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                anim.SetInteger("MoveVec", 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hp -= 10;
        Debug.Log(hp);
        if(hp <= 0)
        {
            hp = 0;
        }
        hpGauge.fillAmount = hp / maxHp;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = anim.GetIKHintPosition(AvatarIKHint.RightElbow);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, lefhHand.position);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, lefhHand.rotation);

        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
    }
}
