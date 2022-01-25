using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSCR : MonoBehaviour
{
    private KnightControl knight;
    [SerializeField] private Button btn;
    [SerializeField] private Button moveBtn;
    private GameObject[] enemies;
    private int killCount = 0;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        knight = FindObjectOfType<KnightControl>();
        btn.onClick.AddListener(KnightAttack);
        moveBtn.onClick.AddListener(NextEnemy);
        knight.delAttack = KnightAttack;
        EnemiesInit();
    }

    void EnemiesInit()
    {
        foreach(GameObject em in enemies)
        {
            em.GetComponent<EnemyControl>().GetKnightDamage(knight.damage);
            em.GetComponent<EnemyControl>().OnDie = NextEnemy;
            em.GetComponent<EnemyControl>().OnDie += () => this.killCount++;
        }
    }

    IEnumerator MoveToEnemy()
    {
        if(this.killCount != 0)
        {
            yield return new WaitForSeconds(2.2f);
        }
        GameObject hero = knight.gameObject;
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
        if(target != null)
        {
            Vector3 pos = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
            knight.MoveKnight(pos);
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
        knight.anim.Play("Attack01", -1, 0);
        yield return new WaitForSeconds(0.42f);
        knight.anim.Play("Attack02", -1, 0);
        yield return new WaitForSeconds(0.835f);
        knight.anim.Play("Idle_Battle", -1, 0);
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1.0f);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1000f);
            Vector3 pos = new Vector3(hit.point.x, 0f, hit.point.z);
            knight.MoveKnight(pos);
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyControl>().DisplayStat();
            }
        }*/
    }
}
