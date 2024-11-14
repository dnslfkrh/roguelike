using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Player player;
    public PlayerHP playerHP;
    public PoolManager pool;

    [Header("Game Info")]
    public float gameTime;
    public float maxGameTime = 600;

    [Header("UI Elements")]
    public GameObject levelUpPanelPrefab;
    private GameObject levelUpPanelInstance;
    private Button[] optionButtons;

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
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        exp = 0;

        Time.timeScale = 0;

        levelUpPanelInstance = Instantiate(levelUpPanelPrefab);

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            levelUpPanelInstance.transform.SetParent(canvas.transform, false);
        }

        levelUpPanelInstance.SetActive(true);

        SetUpgradeOptions();
    }

    private void SetUpgradeOptions()
    {
        // 이게 증강 선택할 때마다 실행돼서 중복 증강이 나옴.. 해결 필요
        List<string> allUpgrades = new List<string> {
            "HP +100",
            "LigeSteal",
            "A",
            "AWDWAD",
            "ADWDAWd",
        };

        List<string> selectedUpgrades = new List<string>();

        int upgradesToSelect = Mathf.Min(5, allUpgrades.Count);

        for (int i = 0; i < upgradesToSelect; i++)
        {
            int randomIndex = Random.Range(0, allUpgrades.Count);
            selectedUpgrades.Add(allUpgrades[randomIndex]);
            allUpgrades.RemoveAt(randomIndex);
        }

        optionButtons = levelUpPanelInstance.GetComponentsInChildren<Button>();

        if (optionButtons == null || optionButtons.Length == 0)
        {
            Debug.LogError("LevelUp Panel does not have any buttons!");
            return;
        }

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < selectedUpgrades.Count)
            {
                Text buttonText = optionButtons[i].GetComponentInChildren<Text>();
                if (buttonText != null)
                {
                    buttonText.text = selectedUpgrades[i];
                }
                else
                {
                    TMPro.TextMeshProUGUI buttonTextTMP = optionButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>();
                    if (buttonTextTMP != null)
                    {
                        buttonTextTMP.text = selectedUpgrades[i];
                    }
                }

                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => ApplyUpgrade(selectedUpgrades[index]));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void ApplyUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "HP +100":
                playerHP.IncreaseHP(100);
                break;
                // 나머지 업그레이드 옵션들 추가
        }

        CloseLevelUpUI();
    }

    private void CloseLevelUpUI()
    {
        levelUpPanelInstance.SetActive(false);
        Time.timeScale = 1;
    }
}
