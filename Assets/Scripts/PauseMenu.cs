using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool SceneLoadingAgain = false;

    public GameObject PauseMenuUI;
    public GameObject DeathMenuUI;


    private bool playerDead = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerDead) {
            if(GamePaused) {
                Resume();
            } 
            else {
                Pause();
            }
        }
    }

    public void OpenDeathMenu() {
        DeathMenuUI.SetActive(true);
        playerDead = true;

        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Resume();
        TerrainGenerator.InitialMapLoaded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }
}
