using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour
{
    public float radius = 1.0f;
    public LayerMask layerMask;
    private bool isDead = false;
    private GameObject targetGo;
    private NavMeshAgent agent;
    private Animator anim;
    Vector3 tempPos;


    private void Start()
    {
        this.Init();
    }

    public void Init()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
        this.anim = this.GetComponent<Animator>();

        this.StartCoroutine(this.UpdatePathRoutine());
    }

    private IEnumerator UpdatePathRoutine()
    {
        while (true)
        {
            if (this.isDead) break;

            yield return new WaitForSeconds(1f);

            if (this.targetGo == null)
            {
                var center = this.transform.position + new Vector3(0, 0.75f, 0);
                Collider[] colliders = Physics.OverlapSphere(center, this.radius, layerMask);

                for (int i = 0; i < colliders.Length; i++)
                {
                    var playerControl = colliders[i].GetComponent<PlayerControl>();

                    if (playerControl != null && !playerControl.isDead)
                    {
                        this.targetGo = playerControl.gameObject;
                        break;
                    }
                }
            }
            else
            {
                //Ÿ���� ��ġ�� ��� �Ѿƿ��� ���ؼ� isMoving ���ǹ��� ����.
                //�׷��� Ÿ���� ������ ���ķδ� Ÿ���� ��ġ�� �������� ���������� �����Ѵ�.
                //�׷��� ������ �þ� ������ ����� ������ �׸��־� �ϴµ�
                //SetDestination �޼��� ��ü�� �̵���Ű�� ����� ������ �־ �ڷ�ƾ�� �Ű� ������.
                if (!isMoving)
                {
                    /*this.isMoving = true;
                    this.anim.SetBool("HasTarget", true);

                    this.agent.SetDestination(targetGo.transform.position);*/

                    if (this.routine != null) this.StopCoroutine(this.routine);
                    this.routine = this.StartCoroutine(this.MoveRoutine());
                }
            }
            yield return null;
        }
    }

    private Coroutine routine;
    private bool isMoving = false;

    private IEnumerator MoveRoutine()
    {
        while (!isMoving)
        {
            var dis = Vector3.Distance(targetGo.transform.position, this.transform.position);

            if (dis <= 1.4f)
            {
                this.anim.SetBool("HasTarget", false);
                this.anim.SetTrigger("Attack");
                Debug.Log("attack");
                this.isMoving = true;
                yield return new WaitForSeconds(2.5f);
                isMoving = false;
                this.targetGo = null;
                break;
            }
            if(dis > 5f)
            {
                if(Vector3.Distance(tempPos, transform.position) <= 1.4f)
                {
                    this.anim.SetBool("HasTarget", false);
                    Debug.Log("lose target");
                    this.targetGo = null;
                    break;
                }
            }
            else
            {
                this.anim.SetBool("HasTarget", true);
                this.agent.SetDestination(targetGo.transform.position);
                tempPos = targetGo.transform.position;
            }
            yield return null;
        }

        //yield return new WaitForSeconds(2.5f);
        
        //this.isMoving = false;
        /*this.targetGo = null;
        Debug.Log("target is null");*/
    }
}