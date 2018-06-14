using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointCounter : MonoBehaviour {

    // stores variables related to points and point generation
    public float points = 0;                 // the amount of points a player has, will increase as they generate points by standing in the CZ
    public float pointMultiplier = 1;        // multiplier for the amount of points a player will earn as they remain inside the control zone
    private bool earningPoints = false;      // bool to determine whether a player is in the zone and earning points or not
    public GameObject CZ;                    // GO that stores the CZ

    // updating the UI when players generate score
    public Text scoreOutputText;             // assigns the UI element that updates the score for the player
    public int pointsInt;                    // variable to hold the points value as an int

    // create a list that stores the game objects of the other players
    public List<GameObject> OtherPlayers;

    // stores the winning indicator
    public GameObject winningIndicator;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Control Zone")                // when you touch the control zone, you begin generating points
        {
            earningPoints = true;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerExit(Collider other)          // when you leave the control zone, stop generating points
    {
        if (other.tag == "Control Zone")
        {
            earningPoints = false;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        pointsInt = (int)points;                                // converts the (float)points into an (int)points
        UpdateUI();                                             // updates the UI based on the pointsInt
        CompareScores();
        if (CZ == null)                                         // if there isn't a CZ in the scene (i.e. after one has been destroyed), finds it
        {
            CZ = GameObject.Find("Control_Zone(Clone)");
        }

        if (earningPoints == true)                              // while the player is earning points, gain points and subtract points from the CZ
        {
            points += pointMultiplier * Time.deltaTime;         // points will tick up as the player remains in the control zone 
            CZ.GetComponent<ControlZone>().currentPoints -= pointMultiplier * Time.deltaTime;   // reduce the points that are remaining in the point pool in the control zone
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void UpdateUI()
    {
        scoreOutputText.text = pointsInt.ToString();                    // updates the score for the player based on the amount of points they have (int)
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // function to turn on and off the winning indicators
    void CompareScores()
    {
        bool isWinning = true;                                          // bool that determines which player is winning
        // compares the scores of other players, and if your score is higher than the other players, sets isWinning is true
        for (int i = 0; i < OtherPlayers.Count; i++)
        {
            if ((isWinning == true) && (pointsInt > OtherPlayers[i].GetComponent<PlayerPointCounter>().pointsInt))
                isWinning = true;
            else
                isWinning = false;
        }
        winningIndicator.SetActive(isWinning);                          // activates the winning indicator based on whether you are winning or not
    }
}
