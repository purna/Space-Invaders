using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public int speed = 5;
    public int damage;
    public string target;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == target)
        {
            if(other.tag == "Player")
            {
                GameManager.playerDead();
            }
            else
            {
                Score.UpdateScore();
            }

            Destroy(other.gameObject);
            GameObject fire = Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
            Destroy(fire, 1f);
            
            Destroy(gameObject);
        }
    }
}
