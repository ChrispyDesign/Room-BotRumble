using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownSpawner : MonoBehaviour {

    public List<GameObject> Players;

    //// get the player game objects
    //public GameObject P1;
    //public GameObject P2;
    //public GameObject P3;
    //public GameObject P4;

    //// get the scores from the player game objects
    //public float P1Score = 0.0f;
    //public float P2Score = 0.0f;
    //public float P3Score = 0.0f;
    //public float P4Score = 0.0f;

    // crown variables
    //public Sprite crown;                // the image of the crown
    //public Vector3 crownPos;            // the position of the crown

    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        //UpdateScores();
        //CheckWhoIsWinning();
        Players.Sort();

    }

    //void UpdateScores()
    //{
    //    //// assign the scores from the player objects
    //    //P1Score = P1.GetComponent<PlayerPointCounter>().pointsInt;
    //    //P2Score = P2.GetComponent<PlayerPointCounter>().pointsInt;
    //    //P3Score = P3.GetComponent<PlayerPointCounter>().pointsInt;
    //    //P4Score = P4.GetComponent<PlayerPointCounter>().pointsInt;
    //}

    //void CheckWhoIsWinning()
    //{
    //    Players.Sort();
    //    //switch ()
    //    //{
    //    //    //case P1Score > P2Score && P1Score > P3Score && P1Score > P4Score:

    //    //}
    //}
}
