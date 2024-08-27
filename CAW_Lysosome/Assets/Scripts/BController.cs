using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
        Cursor.visible = false;
    }

    public void OnBackToCellMapButtonPressed()
    {
        // in future this should go to cell map, acts as a quit button currently
        Application.Quit();
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
