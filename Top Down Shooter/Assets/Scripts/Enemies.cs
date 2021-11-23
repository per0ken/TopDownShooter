using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Lives
{
    public Rigidbody2D rb;
    public GameObject Player;

    private float movementDuration = 2.0f;
    private float waitBeforeMoving = 2.0f;
    private bool Arrived = false;


    void Start()
    {
        Player = GameObject.Find("Player");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) enemyLives--;
        if (collision.gameObject.CompareTag("Bullet")) Destroy(collision.gameObject);
        if (enemyLives <= 0)
        {
            SoundController.enemyDied.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (!Arrived)
        {
            Arrived = true;
            int randomX = Random.Range(-10, 10);
            int randomY = Random.Range(4, -5);
            StartCoroutine(MoveToPoint(new Vector2(randomX,randomY)));
        }

    }

    void FixedUpdate()
    {
        Vector2 Playerr = Player.transform.position;
        Vector2 lookDir = Playerr - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private IEnumerator MoveToPoint(Vector2 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        Arrived = false;
    }
}
