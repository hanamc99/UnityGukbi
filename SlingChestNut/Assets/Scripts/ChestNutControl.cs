using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestNutControl : MonoBehaviour
{
    void Start()
    {

    }

    public void Shoot(Vector3 dir)
    {
        this.GetComponent<Rigidbody>().AddForce(dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {

    }
}
