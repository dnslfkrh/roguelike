using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTryAgain : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }
}
