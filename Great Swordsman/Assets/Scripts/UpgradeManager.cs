using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject levelUpPanelPrefab;

    private GameObject levelUpPanelInstance;
    private Player player;
    private PlayerHP playerHP;
    private Weapon weapon;
    private WeaponManager weaponManager;
    private Button[] optionButtons;

    private static List<string> availableUpgrades = new List<string>
    {
        "Current HP +100",
        "Max HP +200",
        "Player Damage +5",
        "Player Speed +",
        "Add Sword",
        "Increase Weapon Rotation Speed",
        "Double Damage, Half Sword Speed",
        "Increase Weapon Distance From Player",
        "Decrease Weapon Distance From Player",

    };
    private List<string> selectedPreviousUpgrades = new List<string>();

    private void Awake()
    {
        player = GameManager.Instance.player;
        playerHP = GameManager.Instance.playerHP;
        weaponManager = GameManager.Instance.weaponManager;
    }

    private void ApplyUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "Current HP +100":
                playerHP.IncreaseHP("currentHP", 100);
                break;

            case "Max HP +200":
                playerHP.IncreaseHP("maxHP", 200);
                break;

            case "Player Damage +5":
                player.ChangeAttackDamage("+", 5);
                break;

            case "Player Speed +":
                player.IncreaseMoveSpeed(1);
                break;

            case "Add Sword":
                weaponManager.IncreaseWeaponCount();
                break;

            case "Increase Weapon Rotation Speed":
                weaponManager.ChangeRotationSpeed("+", 50);
                break;

            case "Double Damage, Half Sword Speed":
                player.ChangeAttackDamage("*", 2);
                weaponManager.ChangeRotationSpeed("/", 2);
                break;

            case "Increase Weapon Distance From Player":
                weaponManager.ChangeDistanceFromPlayer(0.5f);
                break;

            case "Decrease Weapon Distance From Player":
                weaponManager.ChangeDistanceFromPlayer(-0.5f);
                break;
        }
        availableUpgrades.Remove(upgrade);
        CloseLevelUpUI();
    }

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

    private void CloseLevelUpUI()
    {
        levelUpPanelInstance.SetActive(false);
        Time.timeScale = 1;
    }
}