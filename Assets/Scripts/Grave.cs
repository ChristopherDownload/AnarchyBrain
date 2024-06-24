using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{

    public GameObject Player;
    public GameObject[] Drop;
    public GameObject[] PerkDrop;
    public SpriteRenderer SR;
    public Sprite[] StateSpr;
    public float RegenTimer;
    public float RegetTimerMax;

    void Start()
    {
        Player = GameObject.Find("Player");
        SR = GetComponent<SpriteRenderer>();
        SR.sprite = StateSpr[0];
    }
    void Update()
    {
        if (SR.sprite != StateSpr[0])
        {
            RegenTimer--;
        }
        if (RegenTimer <= 0)
        {
            Regen();
        }
    }

    void Regen()
    {
        SR.sprite = StateSpr[0];
        RegenTimer = RegetTimerMax;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 3 && SR.sprite == StateSpr[0] && Player.GetComponent<Player>().weaponhel == "Shovel")
        { 
            SpawnDrop(); 
            Player.GetComponent<Player>().weaponhel = Player.GetComponent<Player>().DefaultWpn;
            SR.sprite = StateSpr[1];
        }
    }

    public void SpawnDrop()
    {
        float num1 = Random.Range(1, 3);
        if (num1 == 1)
        {
            GameObject p = Instantiate(Drop[Random.Range(0, Drop.Length)], transform.position, transform.rotation);
            p.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
        }
        int rnum = Random.Range(1, 4);
        for (int i = 0; i < rnum; i++)
        {
            GameObject p = Instantiate(PerkDrop[Random.Range(0, PerkDrop.Length)], transform.position, transform.rotation);
            p.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        }
    }

}
