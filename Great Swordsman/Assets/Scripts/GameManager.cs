using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Enemy[] enemyPrefabs;

    private static GameManager instance;
    public Player player;
    public PlayerHP playerHP;
    public PoolManager pool;
    public UpgradeManager upgradeManager;
    public WeaponManager weaponManager;
    public SkillManager skillManager;

    [Header("Game Info")]
    public float gameTime;
    public float maxGameTime = 600;

    [Header("Player Info")]
    public int level;
    public float exp = 0;
    public int[] nextExp = { 10, 20, 30, 60, 150, 210, 280, 360, 450, 600 };

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

    public void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime >= maxGameTime)
        {
            gameTime = maxGameTime;
            GameClear();
        }
    }

    public void GetExp(int value)
    {
        exp += value;

        if (player.extraExp == true)
        {
            exp += 0.5f;
        }

        CheckLevelUp();
    }

    public void CheckLevelUp()
    {
        if (exp >= nextExp[level])
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        exp = 0;

        upgradeManager.ShowLevelUpOptions();
    }

    public void GameClear()
    {
        SceneManager.LoadScene("Clear");
    }

    public void GameDefeat()
    {
        SceneManager.LoadScene("Defeat");
    }
}