using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 moveVec = Vector3.zero;
        moveVec += new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
        moveVec += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if(moveVec.magnitude > 0.01f)
        {
            this.anim.SetBool("isWalking", true);
            Quaternion q = Quaternion.LookRotation(moveVec, Vector3.up);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, q, 0.1f);
            this.transform.position += moveVec.normalized * 5f * Time.deltaTime;
        }
        else
        {
            this.anim.SetBool("isWalking", false);
        }
    }
}
