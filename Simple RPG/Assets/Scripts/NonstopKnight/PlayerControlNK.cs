using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlNK : MonoBehaviour
{
    private bool isMoving = false;
    private bool isDelay = false;
    private float speed = 5f;
    [HideInInspector] public int damage;
    [HideInInspector] public Animator anim;
    private Coroutine routine;
    private float attackRange = 1f;
    public System.Action delAttack;
    public System.Action delPortal;
    public System.Action<int> delGetWeapon;


    void Start()
    {
        this.damage = DataManage.instance.gi.weapon.damage;
        anim = GetComponent<Animator>();
    }

    IEnumerator MoveRoutine(Vector3 pos)
    {
        anim.Play("RunForwardBattle", -1, 0);
        isMoving = true;
        anim.SetBool("IsMoving", isMoving);
        transform.LookAt(pos);

        while (true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(pos, transform.position) <= attackRange)
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
        if (this.routine != null)
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
                delAttack();
                StartCoroutine(AttackDelay());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Finish":
                    delPortal();
                    break;
            case "1":
                SendWeaponID(other.tag);
                break;
            case "2":
                SendWeaponID(other.tag);
                break;
            case "3":
                SendWeaponID(other.tag);
                break;
            case "4":
                SendWeaponID(other.tag);
                break;
        }
    }

    private void SendWeaponID(string num)
    {
        int i = int.Parse(num);
        delGetWeapon(i);
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2f);
        isDelay = false;
    }
}
