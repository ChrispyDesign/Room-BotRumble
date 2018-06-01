using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownSpawner : MonoBehaviour {

    public List<GameObject> Players = new List<GameObject>();

    //// get the player game objects
    //public GameObject P1;
    //public GameObject P2;
    //public GameObject P3;
    //public GameObject P4;

    // get the scores from the player game objects
    //public float P1Score = 0.0f;
    //public float P2Score = 0.0f;
    //public float P3Score = 0.0f;
    //public float P4Score = 0.0f;

    //// crown variables
    //public Sprite crown;                // the image of the crown
    //public Vector3 crownPos;            // the position of the crown

    // variables for scores
    //private int P1 = 0;
    //private int P2 = 0;
    //private int P3 = 0;
    //private int P4 = 0;

    void Start() {



    }

    // Update is called once per frame
    void Update() {
        //UpdateScores();
        //CheckWhoIsWinning();
        //Players.Sort();
        //Players.Sort(Players[].GetComponent<PlayerPointCounter>().pointsInt);

        //    for (int i = 0; i < (Players.Count - 1); i++)
        //    {
        //        Players[i].gameObject.transform.Find("PlayerWinningIndicator").gameObject.SetActive(false);
        //    }

        //    Players[Players.Count - 1].gameObject.transform.Find("PlayerWinningIndicator").gameObject.SetActive(true);       // index is using player count - 1 to avoid overrunning the end of the list
        //}
    }
    void UpdateScores()
    {
        //// assign the scores from the player objects
        //P1Score = P1.GetComponent<PlayerPointCounter>().pointsInt;
        //P2Score = P2.GetComponent<PlayerPointCounter>().pointsInt;
        //P3Score = P3.GetComponent<PlayerPointCounter>().pointsInt;
        //P4Score = P4.GetComponent<PlayerPointCounter>().pointsInt;

        //P1 = Players[0].GetComponent<PlayerPointCounter>().pointsInt;
        //P2 = Players[1].GetComponent<PlayerPointCounter>().pointsInt;
        //P3 = Players[2].GetComponent<PlayerPointCounter>().pointsInt;
        //P4 = Players[3].GetComponent<PlayerPointCounter>().pointsInt;

    }

    void CheckWhoIsWinning()
    {
        
    }

}
