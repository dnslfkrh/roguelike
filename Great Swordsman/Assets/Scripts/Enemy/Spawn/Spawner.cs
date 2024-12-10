using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnInterval = 0.3f;
    public Transform[] spawnPoints;
    float timer;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.Instance.Pool.Get(Random.Range(0, GameManager.Instance.Pool.prefabs.Length));
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
    }
}