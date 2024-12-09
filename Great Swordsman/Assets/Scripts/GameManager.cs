using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Enemy[] enemyPrefabs;
    private static GameManager instance;

    [System.Serializable]
    public class PersistentData
    {
        public string selectedCharacter;
        public string selectedMap;
        public int currentLevel;
        public float currentExp;
    }

    public PersistentData persistentData = new PersistentData();

    public Player Player => FindObjectOfType<Player>();
    public PlayerHP PlayerHP => FindObjectOfType<PlayerHP>();
    public PoolManager Pool => FindObjectOfType<PoolManager>();
    public UpgradeManager UpgradeManager => FindObjectOfType<UpgradeManager>();
    public WeaponManager WeaponManager => FindObjectOfType<WeaponManager>();
    public SkillManager SkillManager => FindObjectOfType<SkillManager>();

    [Header("Game Info")]
    public float gameTime;
    public float maxGameTime = 600;

    [Header("Player Info")]
    public int level;
    public float exp = 0;
    public int[] nextExp = { 3, 3, 3, 5, 5, 5, 5, 5, 5, 5 };

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

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime >= maxGameTime)
        {
            gameTime = maxGameTime;
            GameClear();
        }
    }

    public void SaveSelectedCharacter(string characterName)
    {
        persistentData.selectedCharacter = characterName;
    }

    public void SaveSelectedMap(string mapName)
    {
        persistentData.selectedMap = mapName;
    }

    public void GetExp(int value)
    {
        exp += value;
        if (Player.extraExp == true)
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
        UpgradeManager.ShowLevelUpOptions();
    }

    public void GameClear()
    {
        ResetGame();
        SceneManager.LoadScene("Clear");
    }

    public void GameDefeat()
    {
        ResetGame();
        SceneManager.LoadScene("Defeat");
    }

    private void ResetGame()
    {
        gameTime = 0;
        exp = 0;
        level = 0;
    }
}