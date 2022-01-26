using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlNK : MonoBehaviour
{
    public int hp;
    private Animator anim;
    private Coroutine rtn;
    private int knightDmg;
    private ParticleSystem ps;
    public System.Action OnDie;
    private bool dieOnce = false;

    public void DisplayStat()
    {
        Debug.Log(this.hp);
    }

    public void GetKnightDamage(int d)
    {
        this.knightDmg = d;
    }

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Debug.Log("¥Í¿Ω");
            hp -= this.knightDmg;
            if (hp <= 0 && !dieOnce)
            {
                dieOnce = true;
                Die();
            }
            else if(hp > 0)
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
        OnDie();
        Debug.Log("¡Í±›");
        anim.Play("Die");
        ps.Play();
        StartCoroutine(DestroyAfterLife());
        //if(rtn == null)
        //{
        //    this.rtn = StartCoroutine(DestroyAfterLife());
        //}
    }

    IEnumerator DestroyAfterLife()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }

    void Update()
    {

    }
}
