using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float DMG;
    public float BaseDMG;
    public float AttSpeed;
    public float BaseAttSpeed;
    public float AmmoLeft;
    public float MaxAmmo;
    public float BaseMaxAmmo;
    public float HitRad;
    public float KnockBack;
    public bool Ranged;
    public bool IsHitting;
    public GameObject[] Projectile;
    public Vector3 AddProjSize;
    public Transform[] ShootPos;
    public Color ProjColor;
    public GameObject DropObj;
    public Animator anim;
    public string Name;
    public GameObject Player;
    public float BID = 1;

    // Start is called before the first frame update
    void Start()
    {
        AmmoLeft = MaxAmmo;
        BaseMaxAmmo = MaxAmmo;
        anim = gameObject.GetComponent<Animator>();
        ShootPos[0] = transform.Find("ShootPos");
        Name = gameObject.name;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DMG = BaseDMG + Player.GetComponent<Player>().DMG;
        AttSpeed = BaseAttSpeed + Player.GetComponent<Player>().AttSpeed;
        AddProjSize = Player.GetComponent<Player>().AddProjSize;
        MaxAmmo = BaseMaxAmmo * Player.GetComponent<Player>().AddMaxAmmo;
        if (Player.GetComponent<Player>().weaponhel == Name)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

        if (Ranged == true)
        {
            anim.SetBool("Ranged", true);
        }

        if (Input.GetButton("Fire1") && AmmoLeft > 0)
        {
            anim.SetTrigger("Swing");
        }

        if (AmmoLeft <= 0)
        {
            Player.GetComponent<Player>().weaponhel = Player.GetComponent<Player>().DefaultWpn;
        }

        if (AmmoLeft > MaxAmmo)
        {
            AmmoLeft = MaxAmmo;
        }

        anim.SetFloat("AttSpeed", AttSpeed);
    }

    public void Regen()
    {
        AmmoLeft = MaxAmmo;
    }

    public void Hit()
    {
        if (Projectile != null)
        {
            if (BID == 1)
            {
                for (int i = 0; i < ShootPos.Length; i++)
                {
                    GameObject Proj = Instantiate(Projectile[0], ShootPos[i].position, transform.rotation);
                    if (Ranged == false)
                    {
                        Proj.GetComponent<Projectile>().DMG = DMG / 2;
                    }
                    Proj.GetComponent<Projectile>().DMG += DMG;
                    Proj.GetComponent<Projectile>().Speed += AttSpeed / 2;
                    Proj.GetComponent<SpriteRenderer>().color = ProjColor;
                    Proj.GetComponent<Transform>().localScale += AddProjSize;
                }

            }
            if (BID == 2)
            {
                for (int i = 0; i < ShootPos.Length; i++)
                {
                    GameObject Proj = Instantiate(Projectile[0], ShootPos[i].position, transform.rotation);
                    if (Ranged == false)
                    {
                        Proj.GetComponent<Projectile>().DMG = DMG / 2;
                    }
                    Proj.GetComponent<Projectile>().DMG += DMG;
                    Proj.GetComponent<Projectile>().Speed += AttSpeed;
                    Proj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 255) / 1000, Random.Range(0, 255) / 1000, Random.Range(0, 255) / 1000);
                    Proj.GetComponent<Transform>().localScale += AddProjSize;
                }
            }
            if (BID == 3)
            {
                for (int i = 0; i < ShootPos.Length; i++)
                {
                    GameObject Proj = Instantiate(Projectile[Random.Range(0, 4)], ShootPos[i].position, transform.rotation);
                    Proj.GetComponent<Projectile>().DMG += DMG;
                    Proj.GetComponent<Projectile>().Speed += AttSpeed;
                    Proj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 255) / 1000, Random.Range(0, 255) / 1000, Random.Range(0, 255) / 1000);
                    Proj.GetComponent<Transform>().localScale += AddProjSize;
                    float spps = Random.Range(0, 100);
                    if (spps == 1)
                    {
                        GameObject sProj = Instantiate(Projectile[Projectile.Length - 1], ShootPos[i].position, transform.rotation);
                        sProj.GetComponent<Projectile>().DMG += DMG;
                        sProj.GetComponent<Projectile>().Speed += AttSpeed;
                        sProj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 255) / 1000, Random.Range(0, 255) / 1000, Random.Range(0, 255) / 1000);
                        sProj.GetComponent<Transform>().localScale += AddProjSize;

                    }
                }
            }

            AmmoLeft--;
        }
        for (int i = 0; i < ShootPos.Length; i++)
        {
        Collider2D[] OC = Physics2D.OverlapCircleAll(ShootPos[i].position, HitRad);
            foreach (Collider2D coll in OC)
            {
                if (coll.gameObject.layer == 7)
                {
                    coll.gameObject.GetComponent<Enemy>().TakeDamage(DMG);
                    coll.gameObject.GetComponent<Rigidbody2D>().velocity *= -1.25f;
                }
            }
        }
    }
    public void HitOn()
    {
        IsHitting = true;
    }
    public void HitOff()
    {
        IsHitting = false;
    }
   /* private void OnCollisionEnter2D(Collision2D coll)
    {
        if (IsHitting == true)
        if (coll.gameObject.layer == 7)
        {
        coll.gameObject.GetComponent<Enemy>().TakeDamage(DMG);
        //coll.gameObject.GetComponent<Rigidbody2D>().velocity = transform.position + (transform.position - coll.gameObject.GetComponent<Transform>().position).normalized;
        }
     } */
}
