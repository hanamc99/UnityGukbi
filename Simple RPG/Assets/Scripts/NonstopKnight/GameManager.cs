using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float MAX_RANGE = 14f;
    [SerializeField] GameObject portal;
    [SerializeField] Button moveBtn;
    //[SerializeField] Button saveBtn;
    PlayerControlNK player;
    GameObject[] enemies;
    [SerializeField] GameObject[] monsters = new GameObject[2];
    [SerializeField] GameObject[] bosses = new GameObject[4];
    bool isFirst = true;
    Coroutine nextFloorRoutine;

    void Start()
    {
        this.nextFloorRoutine = null;
        SpawnEnemies();
        player = FindObjectOfType<PlayerControlNK>();
        player.delAttack = KnightAttack;
        player.delGetWeapon = GetWeapon;
        player.delPortal = NextFloor;
        moveBtn.onClick.AddListener(NextEnemy);
        //saveBtn.onClick.AddListener(LetDataSaved);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemiesInit();
    }

    void SpawnEnemies()
    {
        for(int i = 0; i < DataManage.instance.gi.floor; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-MAX_RANGE, MAX_RANGE), 1f, Random.Range(-MAX_RANGE, MAX_RANGE));
            Instantiate(monsters[Random.Range(0, 2)], pos, monsters[0].transform.rotation);
        }
        int k = DataManage.instance.gi.floor;
        int j =  k / 2;
        if (0 < k && k % 2 == 0 && k < 9)
        {
            Vector3 pos = new Vector3(Random.Range(-MAX_RANGE, MAX_RANGE), 1f, Random.Range(-MAX_RANGE, MAX_RANGE));
            Instantiate(bosses[j - 1], pos, bosses[j - 1].transform.rotation);
        }
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
            emc.hp += DataManage.instance.gi.floor;
            emc.OnDie += NextEnemy;
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
        if(this.nextFloorRoutine == null)
        {
            this.nextFloorRoutine = StartCoroutine(MoveSceneDelay());
        }
    }

    IEnumerator MoveSceneDelay()
    {
        DataManage.instance.gi.floor++;
        DataManage.instance.SaveData();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
