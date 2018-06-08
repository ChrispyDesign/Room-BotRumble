using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxScoreAdjuster : MonoBehaviour {

    // find the game object that tracks the number of players
    private GameObject playerNumberTracker;
    public List<bool> playerNumberTrackerLIST;
    private float maxScore;
    private int playerCounter;

    // the public variables for the max scores
    public float maxScoreMultiplier = 15.0f;                                    // the multiplier for the point max


    // Use this for initialization
    void Start () {
        playerNumberTracker = GameObject.Find("Number of Players");
        playerNumberTrackerLIST = playerNumberTracker.GetComponent<NumberOfPlayers>().PlayersPlaying;
        //maxScore = GetComponent<EndGame>().pointMax;
        AdjustScore();
    }

    // based on the number of players, will adjust the score
    void AdjustScore()
    {
        for (int i = 0; i < playerNumberTrackerLIST.Count; i++)
        {
            if (playerNumberTrackerLIST[i] == true)
            {
                playerCounter++;
            }
        }
        maxScore = playerCounter * maxScoreMultiplier;                          // calculates the max score based on the number of players
        GetComponent<EndGame>().pointMax = maxScore;                            // sets the point maximum to that score
    }
}
