using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D lavaCollider = gameObject.GetComponent<Collider2D>();
        if (collision.gameObject.CompareTag("Bullet"))
            Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag("asd"))
            lavaCollider.isTrigger = false;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Collider2D lavaCollider = gameObject.GetComponent<Collider2D>();
        if (other.gameObject.CompareTag("asd"))
        {
            lavaCollider.isTrigger = true;
        }
    }
}
