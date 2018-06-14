using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour {
    
    // game object that stores the number of players and its list
    public GameObject numberOfPlayersGO;
    public List<bool> numberOfPlayersLIST;

    // game objects for the players
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    // text objects for the score UI
    public Text P1Score;
    public Text P2Score;
    public Text P3Score;
    public Text P4Score;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // on awake, finds the number of players
    private void Awake()
    {
        numberOfPlayersGO = GameObject.Find("Number of Players");                                   // finds the number of players from the previous scene
        if (numberOfPlayersGO == null)                                                              // if there is no number of players GO, it creates a new list to prevent an error
            numberOfPlayersLIST = new List<bool>();
        numberOfPlayersLIST = numberOfPlayersGO.GetComponent<NumberOfPlayers>().PlayersPlaying;     // gets the list from the player number tracker
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // based on the list gained from the player number tracker, only activates the players that correspond to who has selected Start
    void Start()
    {
        if (numberOfPlayersLIST[0] == true)                     // if Player 1 has joined the game...
        {
            P1.SetActive(true);                                 // sets Player 1's character to be active
            P1Score.gameObject.SetActive(true);                 // activates the score UI element for Player 1
        }
        if (numberOfPlayersLIST[1] == true)
        {
            P2.SetActive(true);
            P2Score.gameObject.SetActive(true);
        }
        if (numberOfPlayersLIST[2] == true)
        {
            P3.SetActive(true);
            P3Score.gameObject.SetActive(true);
        }
        if (numberOfPlayersLIST[3] == true)
        {
            P4.SetActive(true);
            P4Score.gameObject.SetActive(true);
        }
    }
}
