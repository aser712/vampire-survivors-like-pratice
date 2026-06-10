using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnInterval = 3f;

    [SerializeField] private float minDistance = 10f;
    [SerializeField] private float maxDistance = 15f;

    [SerializeField] private int minSpawn = 1;
    [SerializeField] private int maxSpawn = 5;

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Collider2D hit;
        float angle;
        float distance;
        int howManySpawn = Random.Range(minSpawn, maxSpawn);

        for (int i = 0; i < howManySpawn; i++)
        {
            do
            {
                angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                distance = Random.Range(minDistance, maxDistance);

                hit = Physics2D.OverlapCircle(
                   new Vector3(
                        Mathf.Cos(angle),
                        Mathf.Sin(angle),
                        0
                    ) * distance,
                   1.0f
               );
            } while (hit == null);

            Vector3 spawnPos =
                player.position +
                new Vector3(
                    Mathf.Cos(angle),
                    Mathf.Sin(angle),
                    0
                ) * distance;

            Instantiate(
                enemyPrefab,
                spawnPos,
                Quaternion.identity
            );
        }
    }
}
