using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    CapsuleCollider2D ObjectCollider;
    Rigidbody2D rigidbody;

    private void Start()
    {
        ObjectCollider = GetComponent<CapsuleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock") || collision.CompareTag("Bullet")) // when hits a rock or another Player-fired Bullet instance it stops the current (aka Bullet) gameObject
            rigidbody.velocity = Vector3.zero;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) ObjectCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) ObjectCollider.isTrigger = false;
    }*/
}
