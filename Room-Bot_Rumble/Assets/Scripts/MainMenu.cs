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

    // variables that store the text that will be turned on or off
    public Text blueReadyText;
    public Text redReadyText;
    public Text greenReadyText;
    public Text yellowReadyText;

    private void Update()
    {
        SelectPlayers();
        StartGame();
        QuitGame();
    }

    // function to exit the game
    public void QuitGame()
    {
        Application.Quit();
        if (XCI.GetButtonDown(XboxButton.Back))
        {
            Application.Quit();
        }
    }

    // how many players will be in the game
    public void SelectPlayers()
    {
        // if A is pressed on a controller, that player has now joined the game
        if (XCI.GetButtonDown(XboxButton.A, XboxController.First))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[0] = true;
            blueReadyText.gameObject.SetActive(true);
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

        // if B is pressed on a controller, that player cancels their role in the game
        if (XCI.GetButtonDown(XboxButton.B, XboxController.First))
        {
            NumberOfPlayersList.GetComponent<NumberOfPlayers>().PlayersPlaying[0] = false;
            blueReadyText.gameObject.SetActive(false);
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

    // load the next scene and start the game if Start is pressed by anyone
    public void StartGame()
    {
        if (XCI.GetButtonDown(XboxButton.Start))
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")          // if start is pressed and the current scene is "Main Menu"...
            {
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
            }
        }
    }
}
