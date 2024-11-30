using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Game/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterData> characters;
}

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public Sprite characterSprite;
    public float maxHP;
    public float attackDamage;
    public float moveSpeed;
}
