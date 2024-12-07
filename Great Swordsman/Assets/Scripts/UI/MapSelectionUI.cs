using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapSelectionUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform mapGrid;
    public GameObject mapButtonPrefab;

    [Header("Map Database")]
    public MapDatabase mapDatabase;

    private void Start()
    {
        GenerateMapButtons();
    }

    private void GenerateMapButtons()
    {
        for (int i = 0; i < mapDatabase.maps.Count; i++)
        {
            GameObject mapButton = Instantiate(mapButtonPrefab, mapGrid);
            MapData mapData = mapDatabase.maps[i];
            mapButton.GetComponentInChildren<TextMeshProUGUI>().text = mapData.mapName;

            string mapName = mapData.mapName;
            mapButton.GetComponent<Button>().onClick.AddListener(() => OnMapSelected(mapName));
        }
    }

    public void OnMapSelected(string mapName)
    {
        PlayerPrefs.SetString("SelectedMapName", mapName);

        switch (mapName)
        {
            case "Grass":
                SceneManager.LoadScene("GrassScene");
                break;
            case "Snow":
                SceneManager.LoadScene("SnowScene");
                break;
            case "Lava":
                SceneManager.LoadScene("LavaScene");
                break;
            case "Castle":
                SceneManager.LoadScene("CastleScene");
                break;
            default:
                break;
        }
    }
}