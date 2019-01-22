using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float blobSpawnSpeed;
    public float spawnRadius;
    public int maxAllowedBlobs;
    public GameObject player;
    public GameObject blob;

    private float timeForNextBlob;

    // Start is called before the first frame update
    void Start()
    {
        timeForNextBlob = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver())
        {
            Destroy(player);
            return;
        }

        if (timeForNextBlob < 0.001f)
        {
            SpawnBlob();
            timeForNextBlob = blobSpawnSpeed;
            // Blobs spawn faster and faster
            blobSpawnSpeed -= 0.1f;
            if (blobSpawnSpeed < 1f)
                blobSpawnSpeed = 1f;
        }
        timeForNextBlob -= Time.deltaTime;

    }

    public void SpawnBlob()
    {
        Vector3 pos = player.transform.position + Random.insideUnitSphere * spawnRadius;
        // Check for borders
        pos.y = 0.5f;
        if (pos.x > 5f)
            pos.x = 5f;
        else if (pos.x < -5f)
            pos.x = -5f;
        if (pos.z > 5f)
            pos.z = 5f;
        else if (pos.z < -5f)
            pos.z = -5f;
        Instantiate(blob, pos, Quaternion.identity);
    }

    private bool IsGameOver()
    {
        GameObject[] blobs = GameObject.FindGameObjectsWithTag("blob");
        return blobs.Length > maxAllowedBlobs;
    }
}
