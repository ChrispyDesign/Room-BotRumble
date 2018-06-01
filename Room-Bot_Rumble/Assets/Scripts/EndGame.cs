using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        P1 = GameObject.FindGameObjectWithTag("Player1");
        P2 = GameObject.FindGameObjectWithTag("Player2");
        P3 = GameObject.FindGameObjectWithTag("Player3");
        P4 = GameObject.FindGameObjectWithTag("Player4");
    }
    

    void Update () {

        // once someone reaches the point threshold, the game ends and it displays a congratulatory
        if (P1.GetComponent<PlayerPointCounter>().points >= pointMax)
        {
            endGameScreen.SetActive(true);
            congratulationsText.color = Color.blue;
            congratulationsText.text = "BLUE WINS";
        }
        if (P2.GetComponent<PlayerPointCounter>().points >= pointMax)
        {
            endGameScreen.SetActive(true);
            congratulationsText.color = Color.red;
            congratulationsText.text = "RED WINS";
        }
        if (P3.GetComponent<PlayerPointCounter>().points >= pointMax)
        {
            endGameScreen.SetActive(true);
            congratulationsText.color = Color.green;
            congratulationsText.text = "GREEN WINS";
        }
        if (P4.GetComponent<PlayerPointCounter>().points >= pointMax)
        {
            endGameScreen.SetActive(true);
            congratulationsText.color = Color.yellow;
            congratulationsText.text = "YELLOW WINS";
        }
    }
}
