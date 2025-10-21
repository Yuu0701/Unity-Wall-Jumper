using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float verticalSpace = 3f;
    [SerializeField] private float horizontalSpace = 2.5f;

    [SerializeField] private Transform player;
    [SerializeField] private float highestWallY;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private float destroyOffset = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highestWallY = transform.position.y;
        FirstWall();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y + 10f > highestWallY)
        {
            SpawnWall();
        }

        DestroyWall();
    }

    private void FirstWall()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnWall();
        }
    }
    private void SpawnWall()
    {
        
        float yPos = highestWallY + verticalSpace;

        Instantiate(wallPrefab, new Vector3(-8.5f, yPos, 0), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(8.5f, yPos, 0), Quaternion.identity);

        highestWallY = yPos;
    }

    private void DestroyWall()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall1");

        foreach (GameObject wall in walls)
        {
            if (wall.transform.position.y + destroyOffset < mainCamera.transform.position.y)
            {
                Destroy(wall);
            }
        }
    }
}
