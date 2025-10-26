using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject platform;
    [SerializeField] private float platformSpawnTime = 10f;
    [SerializeField] private float currentTimeUntilPlatform;
    [SerializeField] private float obstacleSpawnTime = 2f;
    [SerializeField] private float currentTimeUntilSpawn;

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
        cameraTopYPos = Camera.main.transform.position.y + Camera.main.orthographicSize;
        cameraBottomYPos = Camera.main.transform.position.y - Camera.main.orthographicSize;

        SpawnLoop();
    }

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
}
