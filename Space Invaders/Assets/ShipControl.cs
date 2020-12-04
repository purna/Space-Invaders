using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 10;

    public GameObject shot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);

        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(shot, transform.position, Quaternion.identity);
        }
    }
}
