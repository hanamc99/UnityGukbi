using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightControl : MonoBehaviour
{
    private Vector3 dir;
    private bool isMoving = false;
    private bool isDelay = false;
    //[HideInInspector] public bool isAttacking = false;
    private float speed = 2f;
    [HideInInspector] public Animator anim;
    private Coroutine routine;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    IEnumerator MoveRoutine(Vector3 pos)
    {
        isMoving = true;
        anim.SetBool("IsMoving", isMoving);
        transform.LookAt(pos);

        while (true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(pos, transform.position) <= 0.3f)
            {
                isMoving = false;
                anim.SetBool("IsMoving", isMoving);
                break;
            }

            yield return null;
        }
    }

    public void MoveKnight(Vector3 pos)
    {
        if(this.routine != null)
        {
            StopCoroutine(this.routine);
        }
        this.routine = StartCoroutine(MoveRoutine(pos));
    }

    private void OnCollisionStay(Collision collision) //Enter
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isMoving = false;
            anim.SetBool("IsMoving", isMoving);
            if (!isDelay)
            {
                isDelay = true;
                StartCoroutine(AttackDelay());
                anim.SetTrigger("IsSingleAttack");
                //isAttacking = true;
                //anim.SetBool("IsAttacking", isAttacking);
            }
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2f);
        isDelay = false;
    }
}
