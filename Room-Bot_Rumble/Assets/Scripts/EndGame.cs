using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public float pointMax = 50.0f;                                              // point threshold

    // game objects for the players
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    // variables for ending the game and the UI
    public GameObject endGameScreen;                                            // the UI element for the end game screen
    public Text congratulationsText;                                            // the congratulations text that will have its colour and message adjusted
    public bool gameHasEnded = false;                                           // bool to track whether the game has ended or not

    // stores the game object that tracks the number of players
    private GameObject playerNumberTracker;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        // finds the players that are in the scene
        P1 = GameObject.FindGameObjectWithTag("Player1");
        P2 = GameObject.FindGameObjectWithTag("Player2");
        P3 = GameObject.FindGameObjectWithTag("Player3");
        P4 = GameObject.FindGameObjectWithTag("Player4");
        // finds the Player Number Tracker
        playerNumberTracker = GameObject.Find("Number of Players");
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Update () {
        EndTheGame();
        EndGameMenu();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // checks if any players have won, and if they have it ends the game
    void EndTheGame()
    {
        // once someone reaches the point threshold, the game ends and it displays a congratulatory message
        // the congratulatory message changes its colour and message based on which player won
        if ((P1 != null) && (P1.GetComponent<PlayerPointCounter>().points >= pointMax))
        {
            endGameScreen.SetActive(true);                                                  // activates the end game UI
            congratulationsText.color = Color.blue;                                         // sets the colour to the appropriate colour based on the player
            congratulationsText.text = "BLUE WINS";                                         // sets the message based on the player who won
            Time.timeScale = 0.0f;                                                          // freezes timescale so gameplay stops
            gameHasEnded = true;                                                            // sets the bool gameHasEnded to true, so the Pause Menu script is no longer active and lets players quit the game or restart
        }
        if ((P2 != null) && (P2.GetComponent<PlayerPointCounter>().points >= pointMax))
        {
            endGameScreen.SetActive(true);
            congratulationsText.color = Color.red;
            congratulationsText.text = "RED WINS";
            Time.timeScale = 0.0f;
            gameHasEnded = true;
        }
        if ((P3 != null) && (P3.GetComponent<PlayerPointCounter>().points >= pointMax))
        {
            endGameScreen.SetActive(true);
            congratulationsText.color = Color.green;
            congratulationsText.text = "GREEN WINS";
            Time.timeScale = 0.0f;
            gameHasEnded = true;
        }
        if ((P4 != null) && (P4.GetComponent<PlayerPointCounter>().points >= pointMax))
        {
            endGameScreen.SetActive(true);
            congratulationsText.color = Color.yellow;
            congratulationsText.text = "YELLOW WINS";
            Time.timeScale = 0.0f;
            gameHasEnded = true;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // once the game has ended, lets the players quit or restart
    void EndGameMenu()
    {
        if (gameHasEnded == true)                           // this menu is only available when the game has ended
        {
            if (XCI.GetButtonDown(XboxButton.Start))        // if the player presses start...
            {
                Destroy(playerNumberTracker);               // destroys the Player Number Tracker, so there is no conflict when we reload the first scene between the trackers
                gameHasEnded = false;                       // the game has not ended anymore 
                SceneManager.LoadScene("MainMenu");         // loads the "Main Menu" scene
            }

            if (XCI.GetButtonDown(XboxButton.Back))         // if the player presses back...
            {
                Debug.Log("Game is quitting");              // console message to let us know it is quitting in editor
                Application.Quit();                         // quits the game
            }
        }
    }
}
