using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform spawnPoint;
    public bool right;
    public float spawnInterval = 3f;

    void Start()
    {
        InvokeRepeating("SpawnPlatform", 0f, spawnInterval);
    }

    void SpawnPlatform()
    {
        GameObject platform = Instantiate(platformPrefab, spawnPoint.position, Quaternion.identity);

        MovingPlatform movingPlatform = platform.GetComponent<MovingPlatform>();
        if (right)
            movingPlatform.direction = Vector3.right;
        else
            movingPlatform.direction = Vector3.left;
    }
}
