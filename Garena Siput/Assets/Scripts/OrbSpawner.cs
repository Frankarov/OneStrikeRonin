using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;
    public float delay;

    private void Start()
    {
        InvokeRepeating("SpawnOrb", 0f, delay);
    }

    private void SpawnOrb()
    {
        GameObject orb = Instantiate(orbPrefab);

    }
}
