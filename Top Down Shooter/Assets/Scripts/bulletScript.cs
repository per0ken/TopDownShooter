using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    Collider ObjectCollider;

    private void Start()
    {
        ObjectCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) ObjectCollider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) ObjectCollider.isTrigger = false;
    }
}
