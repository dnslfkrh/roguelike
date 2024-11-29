using UnityEngine;
using System.Collections.Generic;

public class CharacterStatsManager : MonoBehaviour
{
    private Dictionary<string, PlayerStats> characterStats = new Dictionary<string, PlayerStats>();

    private void Awake()
    {
        InitializeCharacterStats();
    }

    private void InitializeCharacterStats()
    {
        characterStats.Add("Zoro", new PlayerStats(80f, 1000f, 3f));
        characterStats.Add("Tashigi", new PlayerStats(70f, 900f, 2.8f));
        characterStats.Add("Vista", new PlayerStats(85f, 950f, 3.2f));
        characterStats.Add("Shiryu", new PlayerStats(90f, 850f, 3.5f));
        characterStats.Add("Ryuma", new PlayerStats(95f, 1200f, 2.5f));
        characterStats.Add("King", new PlayerStats(100f, 1100f, 2.7f));
    }

    public PlayerStats GetCharacterStats(string characterName)
    {
        return characterStats.TryGetValue(characterName, out var stats) ? stats : null;
    }
}
