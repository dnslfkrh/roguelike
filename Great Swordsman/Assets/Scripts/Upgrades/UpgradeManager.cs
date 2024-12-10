using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Upgrades.Core;
using Upgrades.Health;
using Upgrades.Combat;
using Upgrades.Movement;
using Upgrades.Weapons;
using Upgrades.Skill;

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
            GameManager.Instance.Player,
            GameManager.Instance.PlayerHP,
            GameManager.Instance.WeaponManager,
            GameManager.Instance.SkillManager
        );
    }

    private void InitializeUpgrades()
    {
        availableUpgrades = new List<IUpgrade>
        {
            new CurrentHPUpgrade(),                                 // 1. ���� ü�� ����
            new MaxHPUpgrade(),                                     // 2. �ִ� ü�� ����
            new HPRegenerationUpgrade(),                            // 3. ü�� ��� ����
            new IncreaseHPRegenerationUpgrade(),                    // 4. ü�� ����� ����
            new VampiricUpdate(),                                   // 5. ���� ����
            new VampiricValueUpdate(),                              // 6. ���� ����
            new IncreaseMaxHPDecreaseDamageUpgrade(),               // 7. �ִ� ü�� ���� + ���ݷ� ����
            new DecreaseMaxHPIncreaseDamageUpgrade(),               // 8. �ִ� ü�� ���� + ���ݷ� ����
            new CanSurviveOnceUpgrade(),                            // 9. ü�� 1�� �ѹ� ��Ƽ��
            new PlayerDamageUpgrade(),                              // 10. ���ݷ� ����
            new DoubleDamageHalfSpeedUpgrade(),                     // 11. ���ݷ� �ι� + Į ȸ�� �ӵ� ����
            new DoubleDamageHalfMaxHPUpgrade(),                     // 12. ���ݷ� �ι� + �ִ� ü�� ���丷
            new IncreaseKnockbackForceUpgrade(),                    // 13. �� �˹� �Ÿ� ����
            new FreeExpUpgrade(),                                   // 14. ����ġ �ֱ�
            new SwordRotateDirectionUpgrade(),                      // 15. Į ȸ�� ���� ����
            new ExtraExpUpgrade(),                                  // 16. �߰� ����ġ
            new UnlockIceEffectUpgrade(),                           // 17. ���� �Ӽ� ��� ����
            new UnlockFireEffectUpgrade(),                          // 18. �� �Ӽ� ��� ����
            new PlayerSpeedUpgrade(),                               // 19. �÷��̾� �̵��ӵ� ����
            new PlayerAndWeaponSpeedUpgrade(),                      // 20. �̵� �ӵ��� Į ȸ�� �ӵ� ����
            new IncreasePlayerSpeedAndDecreaseMaxHPUpgrade(),       // 21. �̵� �ӵ� ���� + �ִ� ü�� ����
            new DecreasePlayerSpeedAndIncreaseMaxHPUpgrade(),       // 22. �̵� �ӵ� ���� + �ִ� ü�� ����
            new AddSwordUpgrade(),                                  // 23. Į 1�� �߰�
            new AddSwordsAndDecreaseDamageUpgrade(),                // 24. Į 2�� �߰� + ���� ������ ����
            new WeaponRotationSpeedUpgrade(),                       // 25. Į ȸ�� �ӵ� ����
            new WeaponDistanceUpgrade(0.5f, true),                  // 26. Į ȸ�� �� ����
            new WeaponDistanceUpgrade(-0.5f, false),                // 27. Į ȸ�� �� ����
            new UnlockDashSkillUpgrade(),                           // 28. �뽬 ��ų �������
            new DashSkillDistanceUpgrade(),                         // 29. �뽬 ��ų �̵� �Ÿ� ����
            new DashSkillCooldownUpgrade(),                         // 30. �뽬 ��ų ��Ÿ�� ���� 
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