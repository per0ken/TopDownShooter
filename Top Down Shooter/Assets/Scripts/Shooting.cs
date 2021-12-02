using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool IsImmortal = false;
    public float disabletimer = 0.0f;
    public float disableAfter = 12.9f;
    private float timestamp = 0.0f;
    float perShotDelay = 0.05f;

    public float bulletForce = 20f;
    // Update is called once per frame
    void Update()
    {
        if (IsImmortal == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        if (IsImmortal == true)
        {
            disabletimer += Time.deltaTime;
            if (disabletimer > disableAfter)
            {
                IsImmortal = false;
                disabletimer = 0;
            }
            if (Input.GetMouseButton(0) && Time.time > timestamp)
            {
                timestamp = Time.time + perShotDelay;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                SoundController.playerShot.Invoke();
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                Object.Destroy(bullet, 1.5f);
            }
        }
    }

    public void enableImmortality()
    {
        if (IsImmortal == false)
            IsImmortal = true;   
    }


    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SoundController.playerShot.Invoke();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Object.Destroy(bullet, 1.5f);
    }
}