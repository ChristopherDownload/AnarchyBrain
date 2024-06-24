using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    public float HPMult;
    public float DMG;
    public float BaseDMG;
    public float Speed;
    public float HitTimer;
    public float HitTimerMax;
    public bool Damagable;
    public SpriteRenderer SR;
    public GameObject Particles;
    public float rotspeed;
    public GameObject Player;
    public Transform Pl;
    public string Name;
    public float AttackNum;
    public Rigidbody2D rb;
    public Transform[] ShootPos;
    public GameObject Proj;
    public GameObject Proj2;
    public bool Boss;

    // Start is called before the first frame update
    void Start()
    {
        if (Boss != true)
        {
        HPMult = Random.Range(1, 3f);
        }
        if (HPMult > 1)
        {
            transform.localScale *= 1.2f;
        }
        HP *= HPMult;
        Player = GameObject.Find("Player");
        Pl = Player.GetComponent<Transform>();
        SR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        float hz = Random.Range(-100f, 100f) / 1000f + 0.1f;
        transform.localScale += new Vector3(hz, hz, 0);

        if (Name == "Zombie")
        {
            StartCoroutine(Zombie());
        }
        else if (Name == "Human")
        {
            StartCoroutine(Human());
        }
        else if (Name == "PoliceMan")
        {
            StartCoroutine(PoliceMan());
        }
        else if (Name == "PoliceDrone")
        {
            StartCoroutine(PoliceDrone());
        }
        else if (Name == "CyborgBoss")
        {
            StartCoroutine(CyborgBoss());
        } 
        else if (Name == "FourSideHuman")
        {
            StartCoroutine(FourSideHuman());
        }
    }

    public void TakeDamage(float DMG)
    {
        if (Damagable == true)
        {
            HP -= DMG;
            HitTimer = HitTimerMax;
            if (HP < 0)
            {
                Destroy(gameObject);
                Instantiate(Particles, transform.position, transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //DAMAGE TAKING
        if (HitTimer <= 0)
        {
            HitTimer = 0;
            Damagable = true;
            SR.color = new Color(255, 255, 255);
        }
        if (HitTimer > 0)
        {
            Damagable = false;
            SR.color = Color.red;
            HitTimer--;
        }
        // AI

        Vector3 rot = transform.position - Player.GetComponent<Transform>().position;
        float rott = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        Vector3 smoothrot = Vector3.Lerp(new Vector3(0, 0, transform.rotation.z), new Vector3(0, 0, rott - 90 + 180), rotspeed);
        transform.rotation = Quaternion.Euler(smoothrot);

    }

    public IEnumerator Zombie()
    {
        if (AttackNum == 1)
        {
            float rr = Random.Range(50, 300);
            for (int i = 0; i < rr; i++)
            {
                Vector3 Dir = Pl.position - transform.position;
                rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
                yield return new WaitForSeconds(0.01f);
            }
            AttackNum = Random.Range(1, 4);
        }
        if (AttackNum == 2)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < ShootPos.Length; i++)
            {
                Instantiate(Proj, ShootPos[i].position, ShootPos[i].rotation);
                yield return new WaitForSeconds(0.01f);
            }
            AttackNum = Random.Range(1, 4);
        }
        if (AttackNum == 3)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 2; i++)
            {
                Instantiate(Proj, ShootPos[i].position, transform.rotation);
                yield return new WaitForSeconds(0.01f);
            }
            AttackNum = Random.Range(1, 4);
        }
        AttackNum = Random.Range(1, 4);
        StartCoroutine(Zombie());
    }

    public IEnumerator Human()
    {
        if (AttackNum == 1)
        {
            float rr = Random.Range(30, 150);
            for (int i = 0; i < rr; i++)
            {
                Vector3 Dir = Pl.position - transform.position;
                rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
                yield return new WaitForSeconds(0.01f);
            }
            AttackNum = Random.Range(1, 3);

        }
        if (AttackNum == 2)
        {
            float rr = Random.Range(1, 6);
            for (int i = 0; i < rr; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Instantiate(Proj, ShootPos[Random.Range(0, ShootPos.Length)].position, transform.rotation);
                yield return new WaitForSeconds(0.01f);
            }
            AttackNum = Random.Range(1, 3);
        }
        AttackNum = Random.Range(1, 3);
        StartCoroutine(Human());
    }
    public IEnumerator PoliceMan()
    {
        if (AttackNum == 1)
        {
            float rr = Random.Range(20, 100);
            for (int i = 0; i < rr; i++)
            {
                Vector3 Dir = Pl.position - transform.position;
                rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
                yield return new WaitForSeconds(0.01f);
            }
            AttackNum = Random.Range(1, 3);

        }
        if (AttackNum == 2)
        {
            float rr = Random.Range(1, 5);
            for (int i = 0; i < rr; i++)
            {
                Instantiate(Proj, ShootPos[0].position, transform.rotation);
                yield return new WaitForSeconds(1f);
            }
        }
        AttackNum = Random.Range(1, 3);
        StartCoroutine(PoliceMan());
    }
    public IEnumerator PoliceDrone()
    {
        if (AttackNum == 1)
        {
            float rr = Random.Range(10, 20);
            for (int i = 0; i < rr; i++)
            {
                Vector3 Dir = Pl.position - transform.position;
                rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1f);
            AttackNum = Random.Range(1, 3);

        }
        if (AttackNum == 2)
        {
            float rr = Random.Range(1, 6);
            for (int i = 0; i < rr; i++)
            {
                Instantiate(Proj, ShootPos[0].position, ShootPos[0].rotation);
                Instantiate(Proj, ShootPos[1].position, ShootPos[1].rotation);

                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f);
            AttackNum = Random.Range(1, 3);
        }
        AttackNum = Random.Range(1, 3);
        StartCoroutine(PoliceDrone());
    }
    public IEnumerator CyborgBoss()
    {
        if (AttackNum == 1)
        {
            float rr = Random.Range(30, 100);
            for (int i = 0; i < rr; i++)
            {
                Vector3 Dir = Pl.position - transform.position;
                rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
                yield return new WaitForSeconds(0.001f);
            }
            yield return new WaitForSeconds(0.5f);
            AttackNum = Random.Range(1, 5);
        }
        if (AttackNum == 2)
        {
            float rr = Random.Range(1, 6);
            for (int i = 0; i < rr; i++)
            {
                yield return new WaitForSeconds(0.15f);
                Instantiate(Proj, ShootPos[0].position, transform.rotation);
            }
            yield return new WaitForSeconds(0.5f);
            AttackNum = Random.Range(1, 5);
        }
        if (AttackNum == 3)
        {
            float rr = Random.Range(1, 20);
            for (int i = 0; i < rr; i++)
            {
                yield return new WaitForSeconds(0.1f);
                Instantiate(Proj2, ShootPos[1].position, transform.rotation);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.5f);
            AttackNum = Random.Range(1, 5);
        }
        if (AttackNum == 4)
        {
            yield return new WaitForSeconds(1f);
            Vector3 Dir = Pl.position - transform.position;
            rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed * 10;
            yield return new WaitForSeconds(1f);
        }
        if (AttackNum == 5)
        {
            float rr = Random.Range(10, 30);
            for (int i = 0; i < rr; i++)
            {
                Instantiate(Proj, ShootPos[0].position, transform.rotation);
                Instantiate(Proj2, ShootPos[1].position, transform.rotation);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        if (AttackNum == 6)
        {
            float rr = Random.Range(2, 4);
            yield return new WaitForSeconds(rr);
        }
        AttackNum = Random.Range(1, 7);
        StartCoroutine(CyborgBoss());
    }

    public IEnumerator FourSideHuman()
    {
        if (AttackNum == 1)
        {
            float rr = Random.Range(30, 150);
            for (int i = 0; i < rr; i++)
            {
                Vector3 Dir = Pl.position - transform.position;
                rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
                yield return new WaitForSeconds(0.01f);
            }
            AttackNum = Random.Range(1, 3);
        }
        if (AttackNum == 2)
        {
            float rr = Random.Range(1, 6);
            for (int i = 0; i < rr; i++)
            {
                yield return new WaitForSeconds(0.5f);
                for (int z = 0; z < ShootPos.Length; z++)
                {
                Instantiate(Proj, ShootPos[z].position, ShootPos[z].rotation);
                }
                yield return new WaitForSeconds(0.01f);
            }
            AttackNum = Random.Range(1, 3);
        }
        AttackNum = Random.Range(1, 3);
        StartCoroutine(FourSideHuman());
    }
}
