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
        try
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager Instance is null!");
                return;
            }

            if (GameManager.Instance.Pool == null)
            {
                Debug.LogError("Pool Manager is null!");
                return;
            }

            if (GameManager.Instance.Pool.prefabs.Length == 0)
            {
                Debug.LogError("No prefabs in pool!");
                return;
            }

            GameObject enemy = GameManager.Instance.Pool.Get(Random.Range(0, GameManager.Instance.Pool.prefabs.Length));
            enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in Spawn method: {e.Message}");
        }
    }
}