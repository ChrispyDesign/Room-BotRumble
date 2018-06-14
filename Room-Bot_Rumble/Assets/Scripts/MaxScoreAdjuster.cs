using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxScoreAdjuster : MonoBehaviour {

    // find the game object that tracks the number of players
    private GameObject playerNumberTracker;
    public List<bool> playerNumberTrackerLIST;

    private float maxScore;                                                     // float that stores the score that a player needs to reach in order to win
    private int playerCounter;                                                  // int that stores the number of players that are playing
    public float maxScoreMultiplier = 15.0f;                                    // the multiplier used to determine the maxScore

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start () {
        playerNumberTracker = GameObject.Find("Number of Players");                                     // finds the GO that tracks the number of players
        playerNumberTrackerLIST = playerNumberTracker.GetComponent<NumberOfPlayers>().PlayersPlaying;   // gets the list off of that GO
        AdjustScore();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // based on the number of players, will adjust the score
    void AdjustScore()
    {
        for (int i = 0; i < playerNumberTrackerLIST.Count; i++)                                         // for every player who has readied up...
        {
            if (playerNumberTrackerLIST[i] == true)
            {
                playerCounter++;                                                                        // add 1 to the player counter
            }
        }
        maxScore = playerCounter * maxScoreMultiplier;                          // calculates the max score based on the number of players
        GetComponent<EndGame>().pointMax = maxScore;                            // sets the point maximum to that score
    }
}
