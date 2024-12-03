using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    public string mapName;
}

[CreateAssetMenu(fileName = "MapDatabase", menuName = "Game/Map Database")]
public class MapDatabase : ScriptableObject
{
    public List<MapData> maps = new List<MapData>
    {
        new MapData { mapName = "Grass" },
        new MapData { mapName = "Snow" },
        new MapData { mapName = "Lava" },
        new MapData { mapName = "Castle" }
    };
}