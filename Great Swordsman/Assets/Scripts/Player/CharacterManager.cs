using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Player Object")]
    public GameObject player;

    [Header("Player Components")]
    public SpriteRenderer playerSpriteRenderer;
    public Player playerScript;
    public PlayerHP playerHP;

    [Header("Character Database")]
    public CharacterDatabase characterDatabase;

    private void Start()
    {
        ApplySelectedCharacter();
    }

    private void ApplySelectedCharacter()
    {
        string selectedCharacterName = PlayerPrefs.GetString("SelectedCharacterName");

        CharacterData selectedCharacter = characterDatabase.characters.Find(character => character.characterName == selectedCharacterName);

        if (selectedCharacter != null)
        {
            playerSpriteRenderer.sprite = selectedCharacter.characterSprite;

            playerHP.SetMaxHealth(selectedCharacter.maxHP);
            playerScript.SetMoveSpeed(selectedCharacter.moveSpeed);
            playerScript.SetAttackDamage(selectedCharacter.attackDamage);
        }
        else
        {
            Debug.LogWarning("Selected character not found!");
        }
    }
}