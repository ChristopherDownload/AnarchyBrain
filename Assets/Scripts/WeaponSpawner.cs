using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] WPNS;
    public float Timer;
    public float MaxTimer;
    public float RNum;

    // Start is called before the first frame update
    void Start()
    {
        Timer = MaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Timer--;
        if (Timer <= 0)
        {
            SpawnRandom();
            Timer = MaxTimer;
        }
    }

    public void SpawnRandom()
    {
        RNum = Random.Range(0, WPNS.Length);

        GameObject WPN = Instantiate(WPNS[(int)RNum], transform.position, transform.rotation);
        if (WPN.GetComponent<Pickup>().Unlocked != true)
        {
            Destroy(WPN);
            SpawnRandom();
        }
        WPN.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
    }
}
