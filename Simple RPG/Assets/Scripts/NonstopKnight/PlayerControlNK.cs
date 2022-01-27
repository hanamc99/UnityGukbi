using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlNK : MonoBehaviour
{
    private bool isMoving = false;
    private bool isDelay = false;
    private float speed = 8f;
    [HideInInspector] public Animator anim;
    [SerializeField] GameObject[] weapons = new GameObject[5];
    int equippingWeaponID;
    private Coroutine routine;
    private float attackRange = 1f;
    public System.Action delAttack;
    public System.Action delPortal;
    public System.Action<int> delGetWeapon;


    void Start()
    {
        this.equippingWeaponID = DataManage.instance.gi.weapon.id;
        weapons[equippingWeaponID].SetActive(true);
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
            if (isMoving)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            if (Vector3.Distance(pos, transform.position) <= attackRange)
            {
                isMoving = false;
                anim.SetBool("IsMoving", isMoving);
            }

            if (!isMoving)
            {
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

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isMoving = false;
            anim.SetBool("IsMoving", isMoving);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
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
                EquipWeaponByID(other.tag);
                Destroy(other.gameObject);
                break;
            case "2":
                EquipWeaponByID(other.tag);
                Destroy(other.gameObject);
                break;
            case "3":
                EquipWeaponByID(other.tag);
                Destroy(other.gameObject);
                break;
            case "4":
                EquipWeaponByID(other.tag);
                Destroy(other.gameObject);
                break;
        }
    }

    private void EquipWeaponByID(string num)
    {
        int i = int.Parse(num);
        weapons[equippingWeaponID].SetActive(false);
        weapons[i].SetActive(true);
        this.equippingWeaponID = i;
        delGetWeapon(i);
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2f);
        isDelay = false;
    }
}
