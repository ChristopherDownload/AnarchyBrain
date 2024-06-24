using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject Player;
    public Transform Playe;
    public string Name;
    public bool Unlocked;
    public bool Pickable;
    public bool Perk;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Playe = Player.GetComponent<Transform>();
        StartCoroutine(PickDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (Pickable == true)
        {
            if (coll.gameObject.layer == 3 && Perk != true)
            {
                Playe.Find(Name).gameObject.GetComponent<Weapon>().Regen();
                Player.GetComponent<Player>().weaponhel = Name; 
                Destroy(gameObject);
            }
            else if (coll.gameObject.layer == 3 && Perk == true)
            {
                if (Name == "DMG")
                {
                    Player.GetComponent<Player>().DMG += Random.Range(1, 4);
                }
                if (Name == "Speed")
                {
                    Player.GetComponent<Player>().Speed += Random.Range(1, 3);
                }
                if (Name == "FR")
                {
                    Player.GetComponent<Player>().AttSpeed += Random.Range(1, 100) / 50;
                }
                if (Name == "BS")
                {
                    Player.GetComponent<Player>().AddProjSize += new Vector3(0.3f, 0.3f, 0.3f);
                }
                if (Name == "SizeDown")
                {
                    Player.GetComponent<Transform>().localScale -= new Vector3(0.05f, 0.05f, 0);
                }
                if (Name == "MaxAmmoAdd")
                {
                    Player.GetComponent<Player>().AddMaxAmmo += 0.25f;
                }
                if (Name == "Health")
                {
                    Player.GetComponent<Player>().AddHP(Random.Range(1, 3));
                }
                Destroy(gameObject);
            }
        }
    }
    public IEnumerator PickDelay()
    {
        Pickable = false;
        for (int i = 0; i < 10; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        Pickable = true;
    }
}
