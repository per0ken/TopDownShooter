using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Player;

    private int lives = 3;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) lives--;
        if (collision.gameObject.CompareTag("asd")) lives--;
        if (collision.gameObject.CompareTag("Bullet")) Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag("asd")) Destroy(collision.gameObject);
        if (lives==0) Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        Vector2 Playerr = Player.transform.position;
        Vector2 lookDir = Playerr - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
