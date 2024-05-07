using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    public float minSpawnRate = 2f;
    public float maxSpawnRate = 5f;

    public float minDisappearTime = 5f;
    public float maxDisappearTime = 10f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        SpawnOrb();
    }

    private void SpawnOrb()
    {
        // Set a random spawn rate for the next orb
        float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);

        // Set a random disappear time for the orb
        float disappearTime = Random.Range(minDisappearTime, maxDisappearTime);

        // Set a random position within the main camera's viewport
        Vector3 randomPosition = new Vector3(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            mainCamera.nearClipPlane + 1f
        );

        Vector3 worldPosition = mainCamera.ViewportToWorldPoint(randomPosition);
        transform.position = worldPosition;

        // Schedule the next orb spawn after a delay
        Invoke("SpawnOrb", disappearTime + spawnRate);

    }
}
