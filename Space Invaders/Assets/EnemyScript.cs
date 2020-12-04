using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int speed = 2;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= 6)
        {
            transform.position = new Vector2(transform.position.x - 1, transform.position.y - 1);
            speed = -speed;
            rb.velocity = new Vector2(speed, 0);
        }
        else if(transform.position.x <= -6)
        {
            transform.position = new Vector2(transform.position.x + 1, transform.position.y - 1);
            speed = -speed;
            rb.velocity = new Vector2(speed, 0);
        }
    }

    private void OnBecameVisible()
    {
        GetComponent<EnemyShoot>().enabled = true;
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
