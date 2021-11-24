using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : Enemies
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float bulletForce = 12.5f;

    private float minTime = 1.0f;
    private float maxTime = 3.0f;

    public float spawnTimeInternal;

    private float timer;

    void Start()
    {
        InvokeRepeating("Shoot", 1.5f,spawnTimeInternal);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTimeInternal)
        {
            canShoot = true;
            Shoot();
            spawnTimeInternal = Random.Range(minTime, maxTime);
            timer = 0;
        }
    }

    void Shoot()
    {
        if (canShoot == true)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            Object.Destroy(bullet, 1.5f);
        }
    }
}
