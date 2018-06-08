using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;                // keeps track on whether the game is paused or not
    public GameObject pauseMenuUI;                          // the canvas object for the pause menu

    // find the game object that tracks the number of players
    public GameObject playerNumberTracker;

    private void Start()
    {
        playerNumberTracker = GameObject.Find("Number of Players");
    }
    void Update() {
        if (GetComponent<EndGame>().gameHasEnded == true)
        {
            //Debug.Log("Game has ended");
            return;
        }

        if (XCI.GetButtonDown(XboxButton.Start))            // if the player presses Start...
        {
            if (gameIsPaused == true)                       // if the game is already paused it will resume the game
            {
                Resume();
            }
            else
            {
                Pause();                                    // if the game is not paused it will pause the game
            }
        }
    }

    void Pause()                                           // will freeze time and all the players
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }

    public void Resume()                                           // revert the freeze
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void RestartGame()
    {
        //pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Destroy(playerNumberTracker);
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Application is quitting");
        Application.Quit();
    }
}
