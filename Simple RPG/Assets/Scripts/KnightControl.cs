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


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1.0f);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1000f);
            isMoving = true;
            anim.SetBool("IsMoving", isMoving);
            dir = hit.point;
            dir = new Vector3(dir.x, 0f, dir.z);
            transform.LookAt(dir);
        }
        MoveKnight();
    }

    void MoveKnight()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            float dis = (transform.position - dir).magnitude;
            if (dis <= 0.28f)
            {
                isMoving = false;
                anim.SetBool("IsMoving", isMoving);
            }
        }
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
