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
            hpSlider = GetComponent<Slider>();

        hpSlider.minValue = 0f;
        hpSlider.maxValue = playerHP.MaxHP;

        if (playerHP != null)
        {
            playerHP.onHealthChanged.AddListener(UpdateHealthBar);
        }
    }

    private void Start()
    {
        hpSlider.value = playerHP.CurrentHP;

        if (gradient != null && fill != null)
        {
            fill.color = gradient.Evaluate(1f);
        }
    }

    private void UpdateHealthBar(float healthRatio)
    {
        float currentValue = playerHP.CurrentHP;
        hpSlider.value = currentValue;

        if (gradient != null && fill != null)
        {
            float ratio = Mathf.Clamp01(currentValue / playerHP.MaxHP);
            fill.color = gradient.Evaluate(ratio);
        }
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