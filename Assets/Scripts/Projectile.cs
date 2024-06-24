using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float DMG;
    public float Speed;
    public float lifeTicks;
    public Rigidbody2D rb;
    public Animator Anim;
    public Camera Maincam;
    public Vector3 mousePos;
    public float ID;
    public GameObject Pl;
    public Transform Player;
    public bool Homing;
    public bool Piercing;
    public GameObject Particles;

    // Start is called before the first frame update
    void Start()
    {
        Pl = GameObject.Find("Player");
        Player = Pl.GetComponent<Transform>();
        if (gameObject.GetComponent<Animator>() != null)
        {
            Anim = gameObject.GetComponent<Animator>();
            if (ID == 1)
            {
                Anim.SetFloat("Speed", Speed);
            }
        }
        rb = GetComponent<Rigidbody2D>();
        Maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = Maincam.ScreenToWorldPoint(Input.mousePosition);

        if (ID == 1)
        {
            Vector3 Dir = mousePos - transform.position;
            Vector3 rot = transform.position - mousePos;
            float rott = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
            transform.rotation = Quaternion.Euler(0, 0, rott + 90);
        }
        if (ID == 2)
        {
            float hz = Random.Range(-100, 100) / 100;
            transform.localScale += new Vector3(hz, hz, 0);

            Vector3 Dir = Player.position - transform.position;
            Vector3 rot = transform.position - Player.position;
            float rott = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
            transform.rotation = Quaternion.Euler(0, 0, rott + 90);
        }
        if (ID == 3)
        {
            Vector3 Dir = mousePos - transform.position;
            Vector3 rot = transform.position - mousePos;
            float Off = Random.Range(-10, 10) / 8;
            float rott = Mathf.Atan2(rot.y + Off, rot.x + Off) * Mathf.Rad2Deg;
            rb.velocity = new Vector2(Dir.x + Off, Dir.y + Off).normalized * Speed;
            transform.rotation = Quaternion.Euler(0, 0, rott + 90);
        }
        if (ID == 4)
        {
            float hz = Random.Range(-100, 100) / 100;
            transform.localScale += new Vector3(hz, hz, 0);

            Vector3 Dir = Player.position - transform.position;
            Vector3 rot = transform.position - Player.position;
            float Off = Random.Range(-10, 10) / 8;
            float rott = Mathf.Atan2(rot.y + Off, rot.x + Off) * Mathf.Rad2Deg;
            rb.velocity = new Vector2(Dir.x + Off, Dir.y + Off).normalized * Speed;
            transform.rotation = Quaternion.Euler(0, 0, rott + 90);
        }
        if (ID == 5)
        {
            float hz = Random.Range(-100, 100) / 100;
            transform.localScale += new Vector3(hz, hz, 0);

            rb.velocity = new Vector2(transform.up.x, transform.up.y) * Speed;
        }
        if (ID == 6)
        {
            Vector3 Dir = mousePos - transform.position;
            float Off = Random.Range(-10, 10) / 4;
            rb.velocity = new Vector2(Dir.x + Off, Dir.y + Off).normalized * Speed;
        }
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 7 && gameObject.layer == 8)
        {
            coll.gameObject.GetComponent<Enemy>().TakeDamage(DMG);
            if (Piercing == false)
            {
                Destroy(gameObject);
            }
        }
        if (coll.gameObject.layer == 3 && gameObject.layer == 9)
        {
            Pl.GetComponent<Player>().TakeDamage(DMG);
            Destroy(gameObject);
        }
        if (coll.gameObject.layer == 9 && gameObject.layer == 8 && Player.GetComponent<Player>().WeaponHeld.GetComponent<Weapon>().Ranged == false)
        {
            float r = Random.Range(1, 3);
            if (r == 1)
            {
                Destroy(coll.gameObject);
            }
        }
    }

    public void KillProj()
    {
        if (Particles != null)
        {
        Instantiate(Particles, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (ID == 1 && Homing == true)
        {
            Vector3 Dir = mousePos - transform.position;
            Vector3 rot = transform.position - mousePos;
            float rott = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
            transform.rotation = Quaternion.Euler(0, 0, rott + 90);
        }

        else if (ID == 2 && Homing == true)
        {
            float hz = Random.Range(-100, 100) / 100;
            transform.localScale += new Vector3(hz, hz, 0);

            Vector3 Dir = Player.position - transform.position;
            Vector3 rot = transform.position - Player.position;
            float rott = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            rb.velocity = new Vector2(Dir.x, Dir.y).normalized * Speed;
            transform.rotation = Quaternion.Euler(0, 0, rott + 90);
        }
        if (ID == 6)
        {
            transform.Rotate(0, 0, 100 * Time.deltaTime);
            float v = 0.95f;
            v -= 0.05f;
            if (rb.velocity.x > 0.05f || rb.velocity.y > 0.05f)
            {
                //rb.velocity *= new Vector2(v, v);
            }
        }

        lifeTicks--;
        if (lifeTicks <= 0)
        {
            KillProj();
        }
    }
}