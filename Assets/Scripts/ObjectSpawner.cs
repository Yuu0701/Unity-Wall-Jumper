using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject platform;
    [SerializeField] private float platformSpawnTime = 10f;
    [SerializeField] private float currentTimeUntilPlatform;
    [SerializeField] private float obstacleSpawnTime = 2f;
    [SerializeField] private float currentTimeUntilSpawn;

    // Variables for increasing difficulty
    [SerializeField] private float timeUntilIncreaseDif = 10f;
    [SerializeField] private float minimumObstacleSpawnTime = 1f;
    [SerializeField] private float difficultyTimer;

    // Get Camera offset to spawn walls outside of camera frame
    [SerializeField] private float cameraOffset = 3f;
    [SerializeField] private float destroyOffset = 5f;
    private float cameraTopYPos;
    private float cameraBottomYPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Collect Camera positions
        cameraTopYPos = Camera.main.transform.position.y + Camera.main.orthographicSize;
        cameraBottomYPos = Camera.main.transform.position.y - Camera.main.orthographicSize;

        SpawnLoop();
        DestroyObstacles();
        DestroyPlatform();

        // Increase difficulty over time -> lower the spawn cooldown
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= timeUntilIncreaseDif)
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }
    }

    private void IncreaseDifficulty()
    {
        // As the Spawn Time decreases, choose the minimumObstacle SpawnTime if Spawn Time goes below the threshold
        obstacleSpawnTime = Mathf.Max(minimumObstacleSpawnTime, obstacleSpawnTime - 0.1f);
        Physics2D.gravity *= 1.02f; // Increase the physics gravity for faster falling Sword
    }

    // Spawn platform and obstacles if the spawn cooldown is finished
    private void SpawnLoop()
    {
        currentTimeUntilSpawn += Time.deltaTime;
        currentTimeUntilPlatform += Time.deltaTime;

        if (currentTimeUntilSpawn >= obstacleSpawnTime)
        {
            Spawn();
            currentTimeUntilSpawn = 0f;
        }

        if (currentTimeUntilPlatform >= platformSpawnTime)
        {
            SpawnPlatform();
            currentTimeUntilPlatform = 0f;
        }
    }

    private void Spawn()
    {
        float spawnHeight = cameraTopYPos + cameraOffset;
        float randomX = Random.Range(-17f, 17f); // Random X position
        GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Spawn Object and store its reference
        GameObject spawnedObject = Instantiate(obstacle, new Vector3(randomX, spawnHeight, 0f), Quaternion.identity);
    }

    private void SpawnPlatform()
    {
        float spawnHeight = cameraTopYPos + cameraOffset;
        float randomX = Random.Range(-8f, 8f); // Random X position

        Instantiate(platform, new Vector3(randomX, spawnHeight, 0f), Quaternion.identity);
    }

    // Obstacle here includes the spike obstacle, which is Instantiated in the WallSpawn.cs
    private void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        // For each object, destroy if out of camera frame
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.y < cameraBottomYPos - destroyOffset)
            {
                Destroy(obstacle);
            }
        }
    }

    private void DestroyPlatform()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        // For each object, destroy if out of camera frame
        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.y < cameraBottomYPos - destroyOffset)
            {
                Destroy(platform);
            }
        }
    }
}
