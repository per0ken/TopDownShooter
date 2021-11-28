using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    float movementSpeed = 4f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    Health myHealth;
    int shotCount = 0;
    public int currentScore = 0;
    public int SCORE_STEP = 100;

    public GameUIController gameUIController;

    public UnityEvent<int> enemyKilled = new UnityEvent<int>();

    public List<int> enemiesKilled = new List<int>();

    private void Start()
    {
        myHealth = GetComponent<Health>();
        enemyKilled.AddListener(OnEnemyKill);
    }

    private void OnEnemyKill(int killedEnemy)
    {
        if (enemiesKilled.Contains(killedEnemy))
            return;
        enemiesKilled.Add(killedEnemy);
        currentScore += SCORE_STEP;
        gameUIController.UpdateScore(currentScore);
        PlayerPrefs.SetInt("latestScore", currentScore);
        if (PlayerPrefs.GetInt("hiScore", 0) < currentScore)
            PlayerPrefs.SetInt("hiScore", currentScore);
    }

    private void OnEnable() => shotCount = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            shotCount++;
            if(shotCount % 3 == 0)
                myHealth.ReduceLife();
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
       rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        /*Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),
                                         Mathf.Clamp(transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 1),
                                         transform.position.z);*/
    }
}
