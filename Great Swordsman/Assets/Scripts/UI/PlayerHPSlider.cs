using UnityEngine;
using UnityEngine.UI;

public class PlayerHPSlider : MonoBehaviour
{
    [SerializeField]
    private PlayerHP playerHP;

    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image fill;

    private void Awake()
    {
        if (hpSlider == null)
        {
            hpSlider = GetComponent<Slider>();
        }

        hpSlider.minValue = 0f;

        if (playerHP != null)
        {
            UpdateMaxHP(playerHP.MaxHP);
            playerHP.onHealthChanged.AddListener(UpdateHealthBar);
        }
    }

    private void Start()
    {
        UpdateHealthBar(playerHP.CurrentHP / playerHP.MaxHP);
    }

    private void UpdateHealthBar(float healthRatio)
    {
        hpSlider.value = healthRatio * hpSlider.maxValue;

        if (gradient != null && fill != null)
        {
            fill.color = gradient.Evaluate(healthRatio);
        }
    }

    private void UpdateMaxHP(float maxHP)
    {
        hpSlider.maxValue = maxHP;
    }

    private void OnEnable()
    {
        if (playerHP != null)
        {
            playerHP.onHealthChanged.AddListener(UpdateHealthBar);
            UpdateHealthBar(playerHP.CurrentHP / playerHP.MaxHP);
        }
    }

    private void OnDisable()
    {
        if (playerHP != null)
        {
            playerHP.onHealthChanged.RemoveListener(UpdateHealthBar);
        }
    }
}
