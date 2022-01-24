using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private KnightControl knight;
    private int hp;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        hp = 10;
        knight = FindObjectOfType<KnightControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("����");
        if (other.CompareTag("Sword"))
        {
            anim.SetTrigger("IsHit");
            hp -= 2;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        //knight.isAttacking = false;
        //knight.anim.SetBool("IsAttacking", knight.isAttacking);
        anim.SetTrigger("IsDie");
        knight.anim.ResetTrigger("IsSingleAttack");//���ӸŴ����� �븮�ڷ� �ֱ�?
        StartCoroutine(DestroyAfterLife());
    }

    IEnumerator DestroyAfterLife()
    {
        yield return new WaitForSecondsRealtime(3f);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
