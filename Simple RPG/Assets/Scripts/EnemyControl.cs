using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //private KnightControl knight;
    [SerializeField] private int id;
    [SerializeField] private int hp;
    private Animator anim;
    private Coroutine rtn;
    private int knightDmg;
    private ParticleSystem ps;
    public System.Action OnDie;

    public void LoadStatData(int id, int hp)
    {
        this.id = id;
        this.hp = hp;
    }

    public void DisplayStat()
    {
        Debug.Log(this.hp + ", " + this.id);
    }

    public void GetKnightDamage(int d)
    {
        this.knightDmg = d;
    }

    void Start()
    {
        //knight = FindObjectOfType<KnightControl>();
        ps = GetComponent<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Debug.Log("¥Í¿Ω");
            hp -= this.knightDmg;
            //anim.SetTrigger("IsHit");
            if (hp <= 0)
            {
                Die();
            }
            else
            {
                Hit();
            }
        }
    }

    void Hit()
    {
        anim.Play("GetHit", -1, 0);
        ps.Play();
    }

    void Die()
    {
        //knight.isAttacking = false;
        //knight.anim.SetBool("IsAttacking", knight.isAttacking);
        //anim.SetTrigger("IsDie");
        OnDie();
        anim.Play("Die");
        ps.Play();
        this.rtn = StartCoroutine(DestroyAfterLife());
    }

    IEnumerator DestroyAfterLife()
    {
        yield return new WaitForSecondsRealtime(2.1f);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
