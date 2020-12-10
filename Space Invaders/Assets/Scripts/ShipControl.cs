using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 10;
    public float timeBetweenShots;

    public GameObject shot;

    float shootTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
            return;

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0);

        if(Input.GetButtonDown("Fire1") && shootTimer <= 0)
        {
            Instantiate(shot, transform.position, Quaternion.identity);
            shootTimer = timeBetweenShots;
        }

        shootTimer -= Time.deltaTime;
    }
}
