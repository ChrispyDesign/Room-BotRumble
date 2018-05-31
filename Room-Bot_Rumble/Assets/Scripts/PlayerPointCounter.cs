using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointCounter : MonoBehaviour {

    // points and point generation
    public float points = 0;                 // the amount of points a player has
    public float pointMultiplier = 1;        // multiplier for the amount of points a player will earn as they remain inside the control zone
    private bool earningPoints = false;      // bool to determine whether a player is in the zone and earning points or not
    public GameObject CZ;                    // looks for the control zone and assigns it to this game object

    // updating the UI
    public Text scoreOutputText;             // assigns the UI element that updates the score for the player
    public int pointsInt;                     // variable to hold the points value as an int

    //private float pointTimer = 0.0f;       // will tick up with time delta time to determine how many points a player is earning

    //private void Awake()
    //{
    //    CZ = GameObject.Find("Control_Zone");
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Control Zone")                // when you touch the control zone, you begin generating points
        {
            earningPoints = true;
            //pointTimer = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)          // when you leave the control zone, stop generating points
    {
        if (other.tag == "Control Zone")
        {
            earningPoints = false;
        }
    }

    private void Update()
    {
        pointsInt = (int)points;
        UpdateUI();
        if (CZ == null)                                         // if there isn't a CZ in the scene (i.e. after one has been destroyed), finds it
        {
            //Debug.Log("where's the CZ");
            CZ = GameObject.Find("Control_Zone(Clone)");
        }

        //pointTimer += Time.deltaTime;
        if (earningPoints == true)
        {
            //Debug.Log(points);
            points += pointMultiplier * Time.deltaTime;         // points will tick up as the player remains in the control zone 
            CZ.GetComponent<ControlZone>().currentPoints -= pointMultiplier * Time.deltaTime;   // reduce the points that are remaining in the point pool in the control zone
        }
    }

    void UpdateUI()
    {
        scoreOutputText.text = pointsInt.ToString();
    }

}
