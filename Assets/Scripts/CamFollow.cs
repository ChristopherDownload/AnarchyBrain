using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    private GameObject Player;
    private Rigidbody2D Playerrb;

    public float speed = 0.25f;
    public Vector3 offset;
    public Vector3 mpos;

    void Start()
    {
        Playerrb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 Dir = mpos - transform.position;
        Vector3 pos = new Vector3(Dir.x, Dir.y, 0).normalized + target.position + offset;

        Vector3 smoothpos = Vector3.Lerp(transform.position, pos, speed);
        transform.position = smoothpos;
        
        //gameObject.GetComponent<Camera>().orthographicSize = 8 * (Vector2.Distance(mpos, transform.position) / 9);
    }
}
