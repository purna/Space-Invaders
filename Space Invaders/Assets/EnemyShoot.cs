using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float timeBetweenShots;
    public float nextShot = -1;
    public GameObject bullet;

    public float variation;

    // Start is called before the first frame update
    void Start()
    {
        nextShot = Time.time + Random.Range(1, timeBetweenShots);
        timeBetweenShots += Random.Range(-variation, variation);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
            return;

        if(Time.time > nextShot)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextShot = Time.time + timeBetweenShots;
        }
    }
}
