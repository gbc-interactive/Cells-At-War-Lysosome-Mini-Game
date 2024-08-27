using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public static bool gameIsPaused = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (gameIsPaused)
            {
                case true:
                    Resume();
                    break;
                case false:
                    Pause();
                    break;
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuScene");
        gameIsPaused = false;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

}
