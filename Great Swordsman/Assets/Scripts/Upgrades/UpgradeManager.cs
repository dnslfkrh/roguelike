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
            new CurrentHPUpgrade(),                                 // 1. 현제 체력 증가
            new MaxHPUpgrade(),                                     // 2. 최대 체력 증가
            new HPRegenerationUpgrade(),                            // 3. 체력 재생 시작
            new IncreaseHPRegenerationUpgrade(),                    // 4. 체력 재생량 증가
            new VampiricUpdate(),                                   // 5. 피흡 시작
            new VampiricValueUpdate(),                              // 6. 피흡량 증가
            new IncreaseMaxHPDecreaseDamageUpgrade(),               // 7. 최대 체력 증가 + 공격력 감소
            new DecreaseMaxHPIncreaseDamageUpgrade(),               // 8. 최대 체력 감소 + 공격력 증가
            new CanSurviveOnceUpgrade(),                            // 9. 체력 1로 한번 버티기
            new PlayerDamageUpgrade(),                              // 10. 공격력 증가
            new DoubleDamageHalfSpeedUpgrade(),                     // 11. 공격력 두배 + 칼 회전 속도 절반
            new DoubleDamageHalfMaxHPUpgrade(),                     // 12. 공격력 두배 + 최대 체력 반토막
            new IncreaseKnockbackForceUpgrade(),                    // 13. 적 넉백 거리 증가
            new FreeExpUpgrade(),                                   // 14. 경험치 주기
            new SwordRotateDirectionUpgrade(),                      // 15. 칼 회전 방향 변경
            new ExtraExpUpgrade(),                                  // 16. 추가 경험치
            new UnlockIceEffectUpgrade(),                           // 17. 얼음 속성 잠금 해제
            new UnlockFireEffectUpgrade(),                          // 18. 불 속성 잠금 해제
            new PlayerSpeedUpgrade(),                               // 19. 플레이어 이동속도 증가
            new PlayerAndWeaponSpeedUpgrade(),                      // 20. 이동 속도와 칼 회전 속도 증가
            new IncreasePlayerSpeedAndDecreaseMaxHPUpgrade(),       // 21. 이동 속도 증가 + 최대 체력 감소
            new DecreasePlayerSpeedAndIncreaseMaxHPUpgrade(),       // 22. 이동 속도 감소 + 최대 체력 증가
            new AddSwordUpgrade(),                                  // 23. 칼 1개 추가
            new AddSwordsAndDecreaseDamageUpgrade(),                // 24. 칼 2개 추가 + 개별 데미지 감소
            new WeaponRotationSpeedUpgrade(),                       // 25. 칼 회전 속도 증가
            new WeaponDistanceUpgrade(0.5f, true),                  // 26. 칼 회전 폭 증가
            new WeaponDistanceUpgrade(-0.5f, false),                // 27. 칼 회전 폭 감소
            new UnlockDashSkillUpgrade(),                           // 28. 대쉬 스킬 잠금해제
            new DashSkillDistanceUpgrade(),                         // 29. 대쉬 스킬 이동 거리 증가
            new DashSkillCooldownUpgrade(),                         // 30. 대쉬 스킬 쿨타임 감소 
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