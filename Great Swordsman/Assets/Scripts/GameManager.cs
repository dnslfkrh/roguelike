using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Player player;
    public PoolManager pool;

    [Header("Game Info")]
    public float gameTime;
    public float maxGameTime = 20f;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singletonObj = new GameObject("GameManager");
                    instance = singletonObj.AddComponent<GameManager>();
                    DontDestroyOnLoad(singletonObj);
                }
            }
            return instance;
        }
    }

    [Header("Player Info")]
    public int level;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;

            exp = 0;
        }
    }
}
