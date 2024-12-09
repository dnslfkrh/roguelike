using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    private void Awake()
    {
        InitializePools();
    }

    private void OnEnable()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        if (prefabs == null || prefabs.Length == 0)
        {
            Debug.LogWarning("No prefabs assigned to PoolManager");
            return;
        }

        pools = new List<GameObject>[prefabs.Length];
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        if (pools == null || index < 0 || index >= pools.Length)
        {
            Debug.LogError($"Invalid pool index: {index}");
            return null;
        }

        GameObject select = null;
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            if (prefabs[index] == null)
            {
                Debug.LogError($"Prefab at index {index} is null");
                return null;
            }

            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}