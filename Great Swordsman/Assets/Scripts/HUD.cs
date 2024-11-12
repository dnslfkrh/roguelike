using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Time }
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float currentExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp[GameManager.Instance.level];
                mySlider.value = currentExp / maxExp;

                break;
            case InfoType.Level:
                break;
            case InfoType.Time:
                break;
        }
    }
}
