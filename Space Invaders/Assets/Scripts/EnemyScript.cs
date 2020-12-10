using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int speed = 2;
    public int points;
    Rigidbody2D rb;

    public float edge = 5f;
    bool hitEdge = false;

    public bool present = false;
    int presentCountdown = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if(rb.velocity == Vector2.zero) rb.velocity = new Vector2(speed, 0);

        if (speed > 0) hitEdge = transform.position.x >= edge;
        else if (speed < 0) hitEdge = transform.position.x <= -edge;

        if (hitEdge)
        {
            if(!present)
                transform.position = new Vector2(transform.position.x , transform.position.y - 1);

            if (!present || presentCountdown > 0)
            {
                speed = -speed;
                rb.velocity = new Vector2(speed, 0);
            }

            if (present)
                presentCountdown--;
        }

        if(transform.position.y < 3.5f && GetComponent<EnemyShoot>().enabled == false)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<EnemyShoot>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(collision.gameObject);
            GameManager.playerDead();
        }
    }
}
