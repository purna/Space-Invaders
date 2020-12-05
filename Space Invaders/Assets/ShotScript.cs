using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public int speed = 5;
    public int damage;
    public string target;
    public GameObject explosion;

    public AudioClip[] explosions;
    public AudioClip fireSound;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        source = GetComponent<AudioSource>();
        source.PlayOneShot(fireSound);
    }

    private void Update()
    {
        if (GameManager.pause)
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        else if (!GameManager.pause && GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            Destroy(gameObject);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == target)
        {
            if (other.tag == "Player")
            {
                GameManager.playerDead();
            }
            else
            {
                Score.UpdateScore(other.GetComponent<EnemyScript>().points);
            }

            Destroy(other.gameObject);
            GameObject fire = Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
            fire.GetComponent<AudioSource>().PlayOneShot(explosions[Random.Range(0, explosions.Length)]);
            Destroy(fire, 1f);
            Destroy(gameObject);
        }
    }
}
