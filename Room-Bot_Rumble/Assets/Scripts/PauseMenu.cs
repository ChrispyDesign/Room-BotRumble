using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;                    // keeps track on whether the game is paused or not
    public GameObject pauseMenuUI;                              // the UI element for the pause menu

    // find the game object that tracks the number of players
    public GameObject playerNumberTracker;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // finds the GO that tracks the number of players
    private void Start()
    {
        playerNumberTracker = GameObject.Find("Number of Players");
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Update() {
        if (GetComponent<EndGame>().gameHasEnded == true)           // if the game has ended, this prevents the Pause menu from being usable
        {
            return;
        }

        if (XCI.GetButtonDown(XboxButton.Start))                    // if the player presses Start...
        {
            if (gameIsPaused == true)                               // if the game is already paused it will resume the game
            {
                Resume();
            }
            else
            {
                Pause();                                            // if the game is not paused it will pause the game
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Pause()                                                    // will freeze time and all the players when the pause menu is activated
    {
        pauseMenuUI.SetActive(true);                                // activate the Pause Menu UI
        Time.timeScale = 0.0f;                                      // freezes the timescale
        gameIsPaused = true;                                        // the game knows the game is paused
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void Resume()                                           // revert the freeze and resume play
    {
        pauseMenuUI.SetActive(false);                              // deactivate the pause menu
        Time.timeScale = 1.0f;                                     // resume time
        gameIsPaused = false;                                      // the game knows the game is no longer paused
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // when the restart button is pressed...
    public void RestartGame()
    {
        gameIsPaused = true;                                       // the game is set to be paused
        Destroy(playerNumberTracker);                              // destroys the player number tracker, so it doesn't conflict when we load the main menu
        SceneManager.LoadScene("MainMenu");                        // loads the main menu
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void QuitGame()
    {
        Debug.Log("Application is quitting");                      // console message to let us know the game is quitting in the editor
        Application.Quit();                                        // quits the message
    }
}
