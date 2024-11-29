using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public CharacterDatabase characterDatabase;

    public void SelectCharacter(int index)
    {
        PlayerPrefs.SetInt("SelectedCharacter", index);
    }
}
