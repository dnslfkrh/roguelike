using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Upgrades.Core;
using Upgrades.Health;
using Upgrades.Combat;
using Upgrades.Movement;
using Upgrades.Weapons;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject levelUpPanelPrefab;

    private GameObject levelUpPanelInstance;
    private PlayerComponents playerComponents;
    private Button[] optionButtons;

    private List<IUpgrade> availableUpgrades;

    private void Awake()
    {
        InitializePlayerComponents();
        InitializeUpgrades();
    }

    private void InitializePlayerComponents()
    {
        playerComponents = new PlayerComponents(
            GameManager.Instance.player,
            GameManager.Instance.playerHP,
            GameManager.Instance.weaponManager
        );
    }

    private void InitializeUpgrades()
    {
        availableUpgrades = new List<IUpgrade>
        {
            // �׽�Ʈ�� ���� �ּ� ó��

            // Health Upgrades
            //new CurrentHPUpgrade(),                     // ���� ü�� ����
            //new MaxHPUpgrade(),                         // �ִ� ü�� ����
            //new HPRegenerationUpgrade(),                // ü�� ���
            new IncreaseHPRegenerationUpgrade(),                // ü�� ����� ���� 
            
            //// Combat Upgrades
            //new PlayerDamageUpgrade(),                  // ���ݷ� ����
            //new DoubleDamageHalfSpeedUpgrade(),         // ���ݷ� �ι� + Į ȸ�� �ӵ� ����
            //new DoubleDamageHalfMaxHPUpgrade(),         // ���ݷ� �ι� + �ִ� ü�� ���丷
            
            //// Movement Upgrades
            //new PlayerSpeedUpgrade(),                   // �÷��̾� �̵��ӵ� ����
            //new PlayerAndWeaponSpeedUpgrade(),          // �̵� �ӵ��� Į ȸ�� �ӵ� ����
            //new IncreasePlayerSpeedAndDecreaseMaxHPUpgrade(),           // �̵� �ӵ� ���� + �ִ� ü�� ����
            //new DecreasePlayerSpeedAndIncreaseMaxHPUpgrade(),           // �̵� �ӵ� ���� + �ִ� ü�� ����

            // Weapon Upgrades
            //new AddSwordUpgrade(),                      // Į 1�� �߰�
            new AddSwordsAndDecreaseDamageUpgrade(),    // Į 2�� �߰� + ���� ������ ����
            new WeaponRotationSpeedUpgrade(),           // Į ȸ�� �ӵ� ����
            new WeaponDistanceUpgrade(0.5f, true),      // Į ȸ�� �� ����
            new WeaponDistanceUpgrade(-0.5f, false)     // Į ȸ�� �� ����
        };
    }

    private void ApplyUpgrade(IUpgrade upgrade)
    {
        upgrade.Apply(playerComponents);
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
        List<IUpgrade> selectableUpgrades = GetSelectableUpgrades();
        List<IUpgrade> selectedUpgrades = GetRandomUpgrades(selectableUpgrades);

        optionButtons = levelUpPanelInstance.GetComponentsInChildren<Button>();

        if (optionButtons == null || optionButtons.Length == 0) return;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < selectedUpgrades.Count)
            {
                var upgrade = selectedUpgrades[i];
                SetButtonText(optionButtons[i], $"{upgrade.Name}\n{upgrade.Description}");
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => ApplyUpgrade(upgrade));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private List<IUpgrade> GetSelectableUpgrades()
    {
        return new List<IUpgrade>(availableUpgrades);
    }

    private List<IUpgrade> GetRandomUpgrades(List<IUpgrade> upgradeCopy)
    {
        List<IUpgrade> selectedUpgrades = new List<IUpgrade>();
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