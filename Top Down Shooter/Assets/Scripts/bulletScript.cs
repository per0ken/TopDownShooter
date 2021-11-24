using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    CapsuleCollider2D ObjectCollider;

    private void Start()
    {
        ObjectCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock") || collision.CompareTag("Bullet")) // when hits a rock or another Player-fired Bullet instance it stops the current (aka Bullet) gameObject
            ObjectCollider.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bush"))
            Destroy(this.gameObject);
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
