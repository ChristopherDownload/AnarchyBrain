using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] RareEn;
    public float RareEnRarity;
    public float MinAmt;
    public float MaxAmt;

    //temporary
    public float SpawnTimer;
    public float IncrTimer;
    public float StartTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer--;
        IncrTimer--;
        if (SpawnTimer <= 0)
        {
            SpawnTimer = StartTimer * Random.Range(1f, 3.5f);
            Spawn();
        }
        if (IncrTimer <= 0)
        {
            IncrTimer = StartTimer * Random.Range(2, 6);
            Increase();
        }
    }

    public void Spawn()
    {
        float spps = Random.Range(MinAmt, MaxAmt);
        for (int i = 0; i < spps; i++)
        {
            GameObject P = Instantiate(Enemies[Random.Range(0, Enemies.Length)], transform.position, transform.rotation);
            P.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));

            float r = Random.Range(1, RareEnRarity);
            if (r == 1)
            {
                GameObject R = Instantiate(RareEn[Random.Range(0, RareEn.Length)], transform.position, transform.rotation);
                R.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            }
        }
    }

    public void Increase()
    {
        MinAmt++;
        MaxAmt++;
    }

}
