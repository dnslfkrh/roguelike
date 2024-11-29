using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform characterGrid;
    public GameObject characterButtonPrefab;

    [Header("Character Database")]
    public CharacterDatabase characterDatabase;

    private void Start()
    {
        GenerateCharacterButtons();
    }

    private void GenerateCharacterButtons()
    {
        for (int i = 0; i < characterDatabase.characters.Count; i++)
        {
            GameObject characterButton = Instantiate(characterButtonPrefab, characterGrid);

            CharacterData characterData = characterDatabase.characters[i];

            characterButton.GetComponentInChildren<TextMeshProUGUI>().text = characterData.characterName;

            string characterName = characterData.characterName;
            characterButton.GetComponent<Button>().onClick.AddListener(() => OnCharacterSelected(characterName));
        }
    }

    private void OnCharacterSelected(string characterName)
    {
        PlayerPrefs.SetString("SelectedCharacterName", characterName);
        Debug.Log($"Character {characterName} Selected!");

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
