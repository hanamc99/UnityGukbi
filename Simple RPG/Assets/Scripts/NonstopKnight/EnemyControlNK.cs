using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlNK : MonoBehaviour
{
    public int hp;
    private Animator anim;
    private Coroutine rtn;
    private ParticleSystem ps;
    public System.Action OnDie;
    private bool dieOnce = false;

    public void DisplayStat()
    {
        Debug.Log(this.hp);
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
            hp -= DataManage.instance.gi.weapon.damage;
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
        yield return null;
        yield return new WaitForSecondsRealtime(GetDieClipLength());
        Destroy(gameObject);
    }

    private float GetDieClipLength()
    {
        AnimatorClipInfo[] clipInfos = this.anim.GetCurrentAnimatorClipInfo(0);
        float length = 0f;
        foreach (AnimatorClipInfo aci in clipInfos)
        {
            if(aci.clip.name == "Die")
            {
                length = aci.clip.length;
            }
        }
        return length;
    }

    void Update()
    {

    }
}
