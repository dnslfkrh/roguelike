using UnityEngine;

public class PlayerCustomizer : MonoBehaviour
{
    public CharacterDatabase characterDatabase;
    public SpriteRenderer spriteRenderer;
    public Player playerScript;

    private void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        CharacterData selectedCharacter = characterDatabase.characters[selectedCharacterIndex];

        spriteRenderer.sprite = selectedCharacter.characterSprite;
        playerScript.attackDamage = selectedCharacter.attackDamage;
    }
}