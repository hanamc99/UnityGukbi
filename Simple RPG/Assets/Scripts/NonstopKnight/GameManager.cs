using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    private PlayerControlNK player;
    [SerializeField] private Button moveBtn;
    //[SerializeField] private Button saveBtn;
    private GameObject[] enemies;
    private bool isFirst = true;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = FindObjectOfType<PlayerControlNK>();
        player.delAttack = KnightAttack;
        player.delGetWeapon = GetWeapon;
        player.delPortal = NextFloor;
        moveBtn.onClick.AddListener(NextEnemy);
        //saveBtn.onClick.AddListener(LetDataSaved);
        EnemiesInit();
    }

    void LetDataSaved()
    {
        DataManage.instance.SaveData();
    }

    void EnemiesInit()
    {
        foreach (GameObject em in enemies)
        {
            EnemyControlNK emc = em.GetComponent<EnemyControlNK>();
            //emc.hp += DataManage.instance.gi.floor;
            emc.GetKnightDamage(player.damage);
            emc.OnDie = NextEnemy;
            emc.DisplayStat();
        }
    }

    IEnumerator MoveToEnemy()
    {
        if (!isFirst)
        {
            yield return new WaitForSeconds(2.2f);
        }
        isFirst = false;
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
        else
        {
            Vector3 pos = new Vector3(portal.transform.position.x, 0f, portal.transform.position.z);
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

    void GetWeapon(int i)
    {
        WeaponDataClass data = DataManage.instance.GetWeaponData(i);
        DataManage.instance.gi.weapon = new Weapon(data.id, data.name, data.damage);
    }

    void NextFloor()
    {
        StartCoroutine(MoveSceneDelay());
    }

    IEnumerator MoveSceneDelay()
    {
        DataManage.instance.gi.floor++;
        DataManage.instance.SaveData();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
