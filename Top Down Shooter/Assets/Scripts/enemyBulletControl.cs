using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletControl : MonoBehaviour
{
    CapsuleCollider2D ObjectCollider;
    void Start()
    {
        ObjectCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
            ObjectCollider.isTrigger = false;
        if (collision.CompareTag("Bush"))
            Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
            ObjectCollider.isTrigger = true;
    }
}
