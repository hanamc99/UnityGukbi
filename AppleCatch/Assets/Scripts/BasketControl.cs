using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketControl : MonoBehaviour
{
    [SerializeField] AudioSource audioHost;
    [SerializeField] AudioClip appleSound;
    [SerializeField] AudioClip bombSound;
    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.Round(hit.point.x);
                float z = Mathf.Round(hit.point.z);
                transform.position = new Vector3(x, 0f, z);
                Debug.Log(hit.point);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            Debug.Log(other.tag);
            this.audioHost.PlayOneShot(appleSound);
            gm.point += 100;
        }
        else if (other.CompareTag("Bomb"))
        {
            Debug.Log(other.tag);
            this.audioHost.PlayOneShot(bombSound);
            gm.point /= 2;
        }
        Destroy(other.gameObject);
    }
}
