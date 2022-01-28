using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject bombPrefab;

    float timer = 0f;
    float gnr = 1f;
    float ratio = 0.25f;

    public void SetMembers(float genRate, float ratio)
    {
        this.gnr = genRate;
        this.ratio = ratio;
    }

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= gnr)
        {
            int x = Random.Range(-1, 2);
            int z = Random.Range(-1, 2);

            float dice = Random.Range(0f, 1f);

            if(dice > ratio)
            {
                Instantiate(applePrefab, new Vector3(x, 3f, z), applePrefab.transform.rotation);
            }
            else
            {
            Instantiate(bombPrefab, new Vector3(x, 3f, z), bombPrefab.transform.rotation);
            }

            timer = 0f;
        }
    }
}
