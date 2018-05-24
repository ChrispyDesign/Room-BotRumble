using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerPunching : MonoBehaviour {

    // variables for doing multiple controllers
    public XboxButton punchButton;
    public XboxController controller;                   // allows you to hook up multiple controllers to different players

    // variable control the time and speed of punch
    public float punchDuration = 0.0f;                  // timer that will tick with deltaTime, interacts with punch duration max
    public float punchDurationLimit;                      // the amount of time that a punch will be travel for before it resets
    public float punchSpeed;                            // how fast the punch moves when it gets launched

    // winding up punches
    public float windUpTimer = 0.0f;                   // how long the player has been winding up a punch
    public float windUpMin = 1.0f;                      // amount of time you need to wind up a punch

    // cooldown after throwing a punch
    public bool canWindUp = true;                      // whether player can wind up punch
    public float windUpCD = 1.0f;                       // amount of time after a punch is thrown before you can wind up again

    // throwing a punch
    public bool canPunch = false;                      // whether player can throw a punch after winding it up
    public GameObject fistReset;                        // game object to reset the fist location
    public Rigidbody fistRB;                            // get the fist's rigidbody

    void Start () {
        fistRB = GetComponent<Rigidbody>();             // assign the variable for the fist rigid body
	}
	
	// Update is called once per frame
	void Update ()
    {
        WindUp();                                       // run the WindUp() function
        Punch();                                        // run the Punch() function
        //WindUpCooldown();                               // run the WindUpCooldown()
        windUpTimer += Time.deltaTime;
        if (windUpTimer >= windUpCD)
        {
            canWindUp = true;
        }
    }

    void WindUp()
    {
        if (/*XCI.GetButton(XboxButton.RightBumper)*/ XCI.GetButton(punchButton, controller) && canWindUp == true)      // When the Right Bumper is held down, execute
        {
            Debug.Log("winding up a punch");
            windUpTimer += Time.deltaTime;              // increase the windUpTimer as time passes

            if (windUpTimer >= windUpMin)                // once you have wound up the punch enough...
            {
                Debug.Log("You can punch");
                canPunch = true;                        // lets player fire a punch with the Punch() function
            }
        }
    }                                                       // the player can wind up a punch

    void Punch()                                                            // the player fires a punch
    {
        if (/*XCI.GetButtonUp(XboxButton.RightBumper*/XCI.GetButtonUp(punchButton, controller))                        // When the bumper is released, fire a punch
        {
            if (canPunch == true)                                           // if the player can punch (have wound up enough)
            {
                Debug.Log("Punch is firing");
                punchDuration = 0.0f;                                       // reset the punch duration timer
                canPunch = false;                                           // cannot punch now
                fistRB.AddForce(transform.forward * punchSpeed);            // move the fist forward
                windUpTimer = 0.0f;                                         // reset how long you have to wind up a punch before you can fire
            }
            //Debug.Log("punch duration is ticking");
            canWindUp = false;                                              // make player unable to wind up another punch
            WindUpCooldown();                                               // invoke the WindUpCooldown() function
        }
        // how long the punch will go for
        punchDuration += Time.deltaTime;                                    // punch duration ticks up
        if (punchDuration >= punchDurationLimit)                              // once the punch has been out for long enough...
        {
            //Debug.Log("punch reset");
            transform.position = fistReset.transform.position;          // teleport the punch back to the reset point
            fistRB.velocity = Vector3.zero;                             // reset the velocity of the fist
            fistRB.angularVelocity = Vector3.zero;                      // reset the angular velocity of the fist
        }
    }

    void WindUpCooldown()                                                   // after throwing a punch, there will be a cooldown before you can throw another punch
    {
        Debug.Log("please wait before firing again");
        windUpTimer = 0.0f;
        //windUpTimer += Time.deltaTime;
        //if (windUpTimer >= windUpCD)
        //{
        //    canWindUp = true;
        //}
    }
}
