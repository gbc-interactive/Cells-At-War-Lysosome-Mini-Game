using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnReplayButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
