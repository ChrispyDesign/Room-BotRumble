using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    // point threshold
    public float pointMax = 50.0f;

    // game objects for the players
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    // game object for the "end game screen"
    public GameObject endGameScreen;

    // writing congratulations
    public Text congratulationsText;

    // bool to track whether the game has ended or not
    public bool gameHasEnded = false;

    // find the game object that tracks the number of players
    private GameObject playerNumberTracker;

    private void Start()
    {
        P1 = GameObject.FindGameObjectWithTag("Player1");
        P2 = GameObject.FindGameObjectWithTag("Player2");
        P3 = GameObject.FindGameObjectWithTag("Player3");
        P4 = GameObject.FindGameObjectWithTag("Player4");
        playerNumberTracker = GameObject.Find("Number of Players");
    }
    
    void Update () {
        EndTheGame();
        EndGameMenu();
    }

    // checks if players have won, and if they have it ends the game
    void EndTheGame()
    {
        // once someone reaches the point threshold, the game ends and it displays a congratulatory
        if ((P1 != null) && (P1.GetComponent<PlayerPointCounter>().points >= pointMax))
        {
            endGameScreen.SetActive(true);
            congratulationsText.color = Color.blue;
            congratulationsText.text = "BLUE WINS";
            Time.timeScale = 0.0f;
            gameHasEnded = true;
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

    // once the game has ended, lets the players quit or restart
    void EndGameMenu()
    {
        //Debug.Log(gameHasEnded);
        if (gameHasEnded == true)
        {
            if (XCI.GetButtonDown(XboxButton.Start))
            {
                Destroy(playerNumberTracker);
                gameHasEnded = false;
                SceneManager.LoadScene("MainMenu");
            }

            if (XCI.GetButtonDown(XboxButton.Back))
            {
                Debug.Log("Game is quitting");
                Application.Quit();
            }
        }
    }

}
