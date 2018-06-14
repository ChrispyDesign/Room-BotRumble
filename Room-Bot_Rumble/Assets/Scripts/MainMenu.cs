using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // game object that will persist between scenes, storing the data for the number of players
    public GameObject NumberOfPlayersList;

    // variables that store the text that indicates which player has readied up
    public Text blueReadyText;
    public Text redReadyText;
    public Text greenReadyText;
    public Text yellowReadyText;

    public int playerCounter = 0;                   // will be used to determine whether you can press start to play as long as there is at least 2 players
    public List<bool> playerList;                   // the list from the Number Of Players GO

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // on start, finds the list of players who are playing
    private void Start()
    {
        playerList = NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        SelectPlayers();
        StartGame();
        QuitGame();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // function to exit the game
    public void QuitGame()
    {
        Application.Quit();                         // invoked when the button is pressed on the UI
        Debug.Log("Game is quitting");              // console message to let us know it is quitting in editor
        if (XCI.GetButtonDown(XboxButton.Back))     // also allows players to quit the game by just pressing "Back"
        {
            Debug.Log("Game is quitting");          // console message to let us know it is quitting in editor
            Application.Quit();                     // quits the game
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // lets players "ready up" for the game by pressing "A", and cancel readying up by pressing "B"
    public void SelectPlayers()
    {
        // if A is pressed on a controller, that player has now joined the game
        if (XCI.GetButtonDown(XboxButton.A, XboxController.First))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[0] = true;       // updates the player list based on who pressed "A"
            blueReadyText.gameObject.SetActive(true);                                           // activates the "ready" text based on who pressed "A"
        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Second))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[1] = true;
            redReadyText.gameObject.SetActive(true);
        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Third))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[2] = true;
            greenReadyText.gameObject.SetActive(true);
        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Fourth))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[3] = true;
            yellowReadyText.gameObject.SetActive(true);
        }

        // if B is pressed on a controller, that player cancels them readying up
        if (XCI.GetButtonDown(XboxButton.B, XboxController.First))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[0] = false;      // updates the player list based on who pressed "B"
            blueReadyText.gameObject.SetActive(false);                                          // deactivates the "ready" text based on who pressed "B"
        }
        if (XCI.GetButtonDown(XboxButton.B, XboxController.Second))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[1] = false;
            redReadyText.gameObject.SetActive(false);
        }
        if (XCI.GetButtonDown(XboxButton.B, XboxController.Third))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[2] = false;
            greenReadyText.gameObject.SetActive(false);
        }
        if (XCI.GetButtonDown(XboxButton.B, XboxController.Fourth))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[3] = false;
            yellowReadyText.gameObject.SetActive(false);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // goes through the list to count the number of players who have readied up. This is called in the StartGame() function
    void CountPlayers()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i] == true)
                playerCounter += 1;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // load the next scene and start the game if Start is pressed by at least 2 people
    public void StartGame()
    {
        if (XCI.GetButtonDown(XboxButton.Start))                                            // when start is pressed...
        {
            CountPlayers();                                                                 // counts the number of players who have readied up
            if (playerCounter >= 2)                                                         // if at least 2 players have readied up...
            {
                if (SceneManager.GetActiveScene().name == "MainMenu")                       // if start is pressed and the current scene is "Main Menu"...
                {
                    SceneManager.LoadScene("Main_001_TunnelMiddle", LoadSceneMode.Single);  // loads the level
                }
            }
            playerCounter = 0;                                                              // sets the player count to 0, so it doesn't interfere the next time you press "Start" to start the game
        }
    }
}
