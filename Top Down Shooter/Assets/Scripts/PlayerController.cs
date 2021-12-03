using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    float movementSpeed = 4f;

    public Rigidbody2D rb;
    public Camera cam;

    public GameObject healthPrefab;
    public GameObject immortalPrefab;
    public float timer;
    public float immortaltime;
    public float spawnTimeInternal = 5;
    public float immortalTimer = 30;

    public Shooting Immortal;
    public GameUIController Counting;

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
        Immortal = GetComponent<Shooting>();
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
        if (collision.gameObject.CompareTag("Lava"))
        {
            if (Immortal.IsImmortal == false)
            {
                Destroy(this.gameObject);
                Health.GameOver();
            }
        }
            if (collision.gameObject.CompareTag("EnemyBullet"))
            if (Immortal.IsImmortal == false)
            {
            Destroy(collision.gameObject);
            shotCount++;
            //if(shotCount % 3 == 0)
               //myHealth.ReduceLife();
        }
        if (collision.gameObject.CompareTag("Life"))
        {
            SoundController.healthAdd.Invoke();
            myHealth.RaiseLife();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Immortal"))
        {
            SoundController.immortalOn.Invoke();
            Immortal.enableImmortality();
            GameUIController.startCounting.Invoke();
            RandomSpawner.moreSpawn.Invoke();
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        timer += Time.deltaTime;
        if (timer > spawnTimeInternal)
        {
            lifeSpawn();
            spawnTimeInternal = Random.Range(15, 20);
            timer = 0;
        }

        immortaltime += Time.deltaTime;
        if (immortaltime > immortalTimer)
        {
            immortalSpawn();
            immortalTimer = Random.Range(45, 55);
            immortaltime = 0;
        }
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = (mousePos - rb.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void FixedUpdate()
    {
        /*Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),
                                         Mathf.Clamp(transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 1),
                                         transform.position.z);*/
    }


    void lifeSpawn()
    {
        int randomFunction = Random.Range(0, 2);
        if (randomFunction == 0)
        {
            GameObject lifeSpawn = Instantiate(healthPrefab, GetRandomLeft(), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        if (randomFunction == 1)
        {
            GameObject lifeSpawn = Instantiate(healthPrefab, GetRandomMiddle(), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        if (randomFunction == 2)
        {
            GameObject lifeSpawn = Instantiate(healthPrefab, GetRandomRight(), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }

    void immortalSpawn()
    {
        int randomFunction = Random.Range(0, 2);
        if (randomFunction == 0)
        {
            GameObject immortalSpawn = Instantiate(immortalPrefab, GetRandomLeft(), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        if (randomFunction == 1)
        {
            GameObject immortalSpawn = Instantiate(immortalPrefab, GetRandomMiddle(), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        if (randomFunction == 2)
        {
            GameObject immortalSpawn = Instantiate(immortalPrefab, GetRandomRight(), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }

    Vector2 GetRandomLeft()
    {

        float randomX = Random.Range(-9.5f, -5.0f);
        float randomY = Random.Range(-4.7f, 3.7f);
        return new Vector2(randomX, randomY);
    }

    Vector2 GetRandomMiddle()
    {
        float randomX = Random.Range(-3.3f, 1.4f);
        float randomY = Random.Range(-4.7f, 3.7f);
        return new Vector2(randomX, randomY);
    }

    Vector2 GetRandomRight()
    {
        float randomX = Random.Range(4.7f, 9.4f);
        float randomY = Random.Range(-4.7f, 3.7f);
        return new Vector2(randomX, randomY);
    }
}

