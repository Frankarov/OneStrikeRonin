using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;
    public float delay;
    public Vector2 spawnPos;

    private void Start()
    {
        InvokeRepeating("SpawnOrb", 0f, delay);
    }

    private void SpawnOrb()
    {
        spawnPos =(Vector2) transform.position + Random.insideUnitCircle * 5;
        GameObject orb = Instantiate(orbPrefab, spawnPos, Quaternion.identity);
    }

    private void Update()
    {
        spawnPos = (Vector2)transform.position + Random.insideUnitCircle ;
    }
}
