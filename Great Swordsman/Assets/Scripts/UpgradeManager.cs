using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanelPrefab;
    [SerializeField] private PlayerHP playerHP;
    private GameObject levelUpPanelInstance;
    private Button[] optionButtons;
    private static List<string> availableUpgrades = new List<string>
    {
        "HP +100",
        "Test A",
        "Test B",
        "Test C",
        "Test D",
        "Test E",
        "Test F"
    };
    private List<string> selectedPreviousUpgrades = new List<string>();

    public void ShowLevelUpOptions()
    {
        Time.timeScale = 0;
        levelUpPanelInstance = Instantiate(levelUpPanelPrefab);
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            levelUpPanelInstance.transform.SetParent(canvas.transform, false);
        }
        levelUpPanelInstance.SetActive(true);
        GenerateUpgradeOptions();
    }

    private void GenerateUpgradeOptions()
    {
        List<string> selectableUpgrades = GetSelectableUpgrades();
        List<string> selectedUpgrades = GetRandomUpgrades(selectableUpgrades);
        optionButtons = levelUpPanelInstance.GetComponentsInChildren<Button>();
        if (optionButtons == null || optionButtons.Length == 0)
        {
            Debug.LogError("레벨업 패널에 버튼이 없습니다");
            return;
        }
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < selectedUpgrades.Count)
            {
                SetButtonText(optionButtons[i], selectedUpgrades[i]);
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

    private List<string> GetSelectableUpgrades()
    {
        List<string> selectableUpgrades = new List<string>(availableUpgrades);
        return selectableUpgrades;
    }

    private List<string> GetRandomUpgrades(List<string> upgradeCopy)
    {
        List<string> selectedUpgrades = new List<string>();
        int upgradesToSelect = Mathf.Min(5, upgradeCopy.Count);
        for (int i = 0; i < upgradesToSelect; i++)
        {
            int randomIndex = Random.Range(0, upgradeCopy.Count);
            selectedUpgrades.Add(upgradeCopy[randomIndex]);
            upgradeCopy.RemoveAt(randomIndex);
        }
        return selectedUpgrades;
    }

    private void SetButtonText(Button button, string text)
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.text = text;
        }
        else
        {
            TextMeshProUGUI buttonTextTMP = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonTextTMP != null)
            {
                buttonTextTMP.text = text;
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
                // 추가 업그레이드 케이스 여기에 추가
        }
        availableUpgrades.Remove(upgrade);
        CloseLevelUpUI();
    }

    private void CloseLevelUpUI()
    {
        levelUpPanelInstance.SetActive(false);
        Time.timeScale = 1;
    }
}