using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    // Reference the Wall Prefabs
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject spikes;

    // Gather the initial positions for walls
    private float leftWallXPos = -17.14f;
    private float rightWallXPos = 17.14f;
    private float initialYPos = -5f;

    // Get Camera offset to spawn walls outside of camera frame
    [SerializeField] private float cameraOffset = 2f;
    [SerializeField] private float destroyOffset = 5f;

    [SerializeField] private float spikeSpawnChance = 0.25f;

    // Variables to update Walls' positions and its height
    private Transform previousLeftWall; // Reference to the last left wall prefab spawned
    private Transform previousRightWall; // Reference to the last right wall prefab spawned
    private float wallHeight; // Collect height of wall prefab for calculation

    void Start()
    {
        // Spawn walls and create variables for instantiating walls that can be used to update Transform variables
        GameObject initialLeftWall = Instantiate(leftWall, new Vector3(leftWallXPos, initialYPos, 0f), Quaternion.identity);
        GameObject initialRightWall = Instantiate(rightWall, new Vector3(rightWallXPos, initialYPos, 0f), Quaternion.identity);

        // Collect the latest wall prefabs transform positions
        previousLeftWall = initialLeftWall.transform;
        previousRightWall = initialRightWall.transform;

        // Get the height of wall's box collider -> i.e. the height of the wall
        wallHeight = initialLeftWall.GetComponent<BoxCollider2D>().bounds.size.y;
    }

    // Keep updating main camera's positions and spawn walls accordingly
    void Update()
    {
        // Camera's top frame height is => current camera center's height + its orthogonal size
        float cameraTopYPos = Camera.main.transform.position.y + Camera.main.orthographicSize;
        float cameraBottomYPos = Camera.main.transform.position.y - Camera.main.orthographicSize;

        // Spawn new walls if the camera frame gets closer to the last Instantied walls
        if (cameraTopYPos > previousLeftWall.position.y - cameraOffset)
        {
            SpawnWall();
        }

        DestroyWall(cameraBottomYPos);
    }

    private void SpawnWall()
    {
        // Calculate next y position of spawning wall
        float nextYPos = wallHeight + previousLeftWall.position.y;

        // Spawn the next walls and store its references
        GameObject nextLeftWall = Instantiate(leftWall, new Vector3(leftWallXPos, nextYPos, 0f), Quaternion.identity);
        GameObject nextRightWall = Instantiate(rightWall, new Vector3(rightWallXPos, nextYPos, 0f), Quaternion.identity);

        // Occasionally spawn spikes on wall
        SpawnSpikes(nextLeftWall.transform, true);
        SpawnSpikes(nextRightWall.transform, false);

        // Update the latest wall's transform positions
        previousLeftWall = nextLeftWall.transform;
        previousRightWall = nextRightWall.transform;
    }

    private void SpawnSpikes(Transform wall, bool isLeftWall)
    {
        // Spawn the Spikes at a random chance
        if (Random.value < spikeSpawnChance)
        {
            Vector3 wallPos = wall.position;

            GameObject spikeInstance = Instantiate(spikes, wallPos, spikes.transform.rotation);

            // If the spike is getting spawned on left wall, flip the prefab
            if (isLeftWall)
            {
                Vector3 localScale = spikeInstance.transform.localScale;
                localScale.y *= -1;
                spikeInstance.transform.localScale = localScale;
            }
        }
    }

    private void DestroyWall(float cameraBottomY)
    {
        // Find all Wall on the Scene view
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

        // For each found wall, destroy if its below the camera frame
        foreach (GameObject wall in walls)
        {
            if (wall.transform.position.y + cameraOffset < Camera.main.transform.position.y)
            {
                // Grab the height of current wall's highest y-position
                float yPos = wall.GetComponent<BoxCollider2D>().bounds.max.y;

                if (yPos < cameraBottomY - destroyOffset)
                {
                    Destroy(wall);
                }
            }
        }
    }
}
