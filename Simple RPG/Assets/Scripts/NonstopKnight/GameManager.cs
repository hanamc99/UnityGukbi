using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerControlNK player;
    [SerializeField] private Button moveBtn;
    //[SerializeField] private Button saveBtn;
    private GameObject[] enemies;
    private int killCount = 0;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = FindObjectOfType<PlayerControlNK>();
        moveBtn.onClick.AddListener(NextEnemy);
        //saveBtn.onClick.AddListener(LetDataSaved);
        player.delAttack = KnightAttack;
        EnemiesInit();
    }

    void LetDataSaved()
    {
        useJson.instance.SaveData();
    }

    void EnemiesInit()
    {
        foreach (GameObject em in enemies)
        {
            //Monster monsterInfo = useJson.instance.gi.monsterInfo;
            EnemyControlNK emc = em.GetComponent<EnemyControlNK>();
            //emc.LoadStatData(monsterInfo.id, monsterInfo.hp);
            emc.GetKnightDamage(player.damage);
            emc.OnDie = NextEnemy;
            emc.OnDie += () => this.killCount++;
            emc.DisplayStat();
        }
    }

    IEnumerator MoveToEnemy()
    {
        if (this.killCount != 0)
        {
            yield return new WaitForSeconds(2.2f);
        }
        GameObject hero = player.gameObject;
        float minDis = 100f;
        GameObject target = null;
        foreach (GameObject em in enemies)
        {
            if (em != null)
            {
                float dis = Vector3.Distance(em.transform.position, hero.transform.position);
                if (dis < minDis)
                {
                    target = em;
                    minDis = dis;
                }
            }
        }
        if (target != null)
        {
            Vector3 pos = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
            player.MoveKnight(pos);
        }
    }

    void NextEnemy()
    {
        StartCoroutine(MoveToEnemy());
    }

    void KnightAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        player.anim.Play("Attack01", -1, 0);
        yield return new WaitForSeconds(0.42f);
        player.anim.Play("Attack02", -1, 0);
        yield return new WaitForSeconds(0.835f);
        player.anim.Play("Idle_Battle", -1, 0);
    }
}
