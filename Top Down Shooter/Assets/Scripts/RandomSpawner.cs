using UnityEngine;
using UnityEngine.Events;

public class RandomSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    Transform spawn;

    private float minTime = 1.5f;
    private float maxTime = 1.75f;

    private float spawnTimer = 0;

    public float spawnTimeInternal;
    private float timer;
    private int spawned = 0;
    private bool spawningMore = false;

    public static UnityEvent moreSpawn = new UnityEvent();

    private void OnEnable()
    {
        moreSpawn.AddListener(spawnMore);
        InvokeRepeating("Spawn", 2.0f, spawnTimeInternal);
    }
    void Update()
    {
        if (spawningMore == false)
        {
            timer += Time.deltaTime;
            if (timer > spawnTimeInternal)
            {
                Spawn();
                spawnTimeInternal = Random.Range(minTime, maxTime);
                timer = 0;
                spawned++;
            }
        }

        if (spawningMore == true)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > 12)
                spawningMore = false;
            timer += Time.deltaTime;
            if (timer > spawnTimeInternal)
            {
                Spawn();
                spawnTimeInternal = 0.5f;
                timer = 0;
                spawned++;
            }
        }

    }

    void spawnMore()
    {
        spawningMore = true;
    }
    void Spawn()
    {
        int randomFunction = Random.Range(0, 3);
        if (randomFunction == 0)
        {
            GameObject enemySpawn = Instantiate(enemyPrefab, GetRandomBottom(), transform.rotation);
        }
        if (randomFunction == 1)
        {
            GameObject enemySpawn = Instantiate(enemyPrefab, GetRandomTop(), transform.rotation);
        }
        if (randomFunction == 2)
        {
            GameObject enemySpawn = Instantiate(enemyPrefab, GetRandomLeft(), transform.rotation);
        }
        if (randomFunction == 3)
        {
            GameObject enemySpawn = Instantiate(enemyPrefab, GetRandomRight(), transform.rotation);
        }
    }

    Vector2 GetRandomBottom()
    {

        int randomX = Random.Range(-13, 13);
        int randomY = Random.Range(-9, -7);
        return new Vector2(randomX, randomY);
    }

    Vector2 GetRandomTop()
    {
        int randomX = Random.Range(-13, 13);
        int randomY = Random.Range(7, 9);
        return new Vector2(randomX, randomY);
    }

    Vector2 GetRandomLeft()
    {
        int randomX = Random.Range(-15, -13);
        int randomY = Random.Range(7, -7);
        return new Vector2(randomX, randomY);
    }

    Vector2 GetRandomRight()
    {
        int randomX = Random.Range(13, 15);
        int randomY = Random.Range(7, -7);
        return new Vector2(randomX, randomY);
    }
}