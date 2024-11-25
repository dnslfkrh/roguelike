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
            // 테스트를 위한 주석 처리

            // Health Upgrades
            //new CurrentHPUpgrade(),                     // 현제 체력 증가
            //new MaxHPUpgrade(),                         // 최대 체력 증가
            //new HPRegenerationUpgrade(),                // 체력 재생
            new IncreaseHPRegenerationUpgrade(),                // 체력 재생량 증가 
            
            //// Combat Upgrades
            //new PlayerDamageUpgrade(),                  // 공격력 증가
            //new DoubleDamageHalfSpeedUpgrade(),         // 공격력 두배 + 칼 회전 속도 절반
            //new DoubleDamageHalfMaxHPUpgrade(),         // 공격력 두배 + 최대 체력 반토막
            
            //// Movement Upgrades
            //new PlayerSpeedUpgrade(),                   // 플레이어 이동속도 증가
            //new PlayerAndWeaponSpeedUpgrade(),          // 이동 속도와 칼 회전 속도 증가
            //new IncreasePlayerSpeedAndDecreaseMaxHPUpgrade(),           // 이동 속도 증가 + 최대 체력 감소
            //new DecreasePlayerSpeedAndIncreaseMaxHPUpgrade(),           // 이동 속도 감소 + 최대 체력 증가

            // Weapon Upgrades
            //new AddSwordUpgrade(),                      // 칼 1개 추가
            new AddSwordsAndDecreaseDamageUpgrade(),    // 칼 2개 추가 + 개별 데미지 감소
            new WeaponRotationSpeedUpgrade(),           // 칼 회전 속도 증가
            new WeaponDistanceUpgrade(0.5f, true),      // 칼 회전 폭 증가
            new WeaponDistanceUpgrade(-0.5f, false)     // 칼 회전 폭 감소
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