using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    public float DMG;
    public float AttSpeed;
    public Vector3 AddProjSize;
    public float AddMaxAmmo;
    private Transform Pl;
    public Vector3 mpos;
    public float Horiz;
    public float Vert;
    public float Speed;
    public bool CanMove = true;
    public Rigidbody2D rb;
    public string weaponhel;
    public string DefaultWpn;
    public GameObject WeaponHeld;
    public SpriteRenderer SR;
    public bool vulnerable;
    public float InvulnFrames;
    public float InvFrameDur;

    // Start is called before the first frame update
    void Start()
    {
        Pl = GameObject.Find("Player").GetComponent<Transform>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Dir = mpos - transform.position;
        Vector3 rot = transform.position - mpos;
        float rott = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        Pl.rotation = Quaternion.Euler(0, 0, rott - 90 + 180);

        Horiz = Input.GetAxisRaw("Horizontal");
        Vert = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Horizontal") && CanMove == true)
        {
            rb.velocity = new Vector2(Horiz * Speed, rb.velocity.y);
        }
        if (Input.GetButton("Vertical") && CanMove == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, Vert * Speed);
        }

        if (WeaponHeld != transform.Find(weaponhel))
        {
            WeaponHeld = transform.Find(weaponhel).gameObject;
            WeaponHeld.SetActive(true);
        }

        if (HP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void TakeDamage(float DMG)
    {
        if (vulnerable == true)
        {
        HP -= DMG;
        SR.color -= new Color(0, 0.25f * DMG, 0.25f * DMG, 0);
        StartCoroutine(Invulnerability());
        }
    }

    public void AddHP(float Plus)
    {
        HP += Plus;
        SR.color += new Color(0, 0.25f * Plus, 0.25f * Plus, 0);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 7 && gameObject.layer == 3)
        {
            TakeDamage(Random.Range(1, 2));
        }
    }

    public IEnumerator Invulnerability()
    {
        vulnerable = false;
        for (int i = 0; i < InvulnFrames; i++)
        {
            SR.enabled = false;
            yield return new WaitForSeconds(InvFrameDur);
            SR.enabled = true;
            yield return new WaitForSeconds(InvFrameDur);
        }
        vulnerable = true;
    }

}
