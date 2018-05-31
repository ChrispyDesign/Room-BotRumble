using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerPunching : MonoBehaviour {

    // variables for doing multiple controllers
    public XboxController controller;                   // allows you to hook up multiple controllers to different players
    public XboxButton punchButton;                      // assigns the button that is used to control the punches
    public XboxAxis punchStickX;                        // assigns the stick used to aim punches
    public XboxAxis punchStickY;                        // assigns the stick used to aim punches

    // variable control the time and speed of punch
    private float punchDuration = 0.0f;                  // timer that will tick with deltaTime, interacts with punch duration max
    public float punchDurationLimit = 0.5f;             // the amount of time that a punch will be travel for before it resets
    public float punchSpeed = 1000f;                    // how fast the punch moves when it gets launched

    // winding up punches
    private float windUpTimer = 0.0f;                   // how long the player has been winding up a punch
    public float windUpMin = 1.0f;                      // amount of time you need to wind up a punch

    // cooldown after throwing a punch
    public bool canWindUp = true;                      // whether player can wind up punch
    public float windUpCD = 1.0f;                       // amount of time after a punch is thrown before you can wind up again

    // throwing a punch
    private bool canPunch = false;                      // whether player can throw a punch after winding it up
    public Transform fistReset;                         // game object to reset the fist location
    public Rigidbody fistRB;                            // get the fist's rigidbody
    public SphereCollider fist;                         // grabs the sphere collider for the fist

    // aiming a punch
    public Vector3 previousAimDirection = Vector3.forward;         // initialises the aiming of the punch to directly in front of you
    //public Color aimDirectionColour = Color.black;               // draws a line where the player is aiming for the Debug.DrawLine()
    public Transform aimIndicatorOrigin;                           // where the aim indicators will originate from
    public LineRenderer aimLine;                                   // the line renderer for the aim indicator
    private Vector3 punchVector;                                   // where a punch is being aimed

    // knocking back a player with a punch
    public float knockback = 2000f;

    // drawing a line from the body to the fists
    public LineRenderer punchLine;                        // the line renderer for the fists

    void Start () {
        fistRB = GetComponent<Rigidbody>();             // assign the variable for the fist rigid body
        fist.enabled = false;
	}
	
	void Update ()
    {
        WindUp();                                       // run the WindUp() function
        Punch();                                        // run the Punch() function
        //WindUpCooldown();                             // run the WindUpCooldown()
        windUpTimer += Time.deltaTime;                  // ticks up the Wind Up Timer
        if (windUpTimer >= windUpCD)                    // if punch wind up is off cooldown, allows the player to wind up a punch
        {
            canWindUp = true;
        }
        AimPunch();                                     // run the aim punch function
        UpdateLines();                                  // draw lines in the game from the player to the fists
        UpdateAimIndicator();                           // draw an indicator that shows where the player is aiming
    }

    // allow the player to wind up a punch before they can fire it
    void WindUp()
    {
        if (/*XCI.GetButton(XboxButton.RightBumper)*/ XCI.GetButton(punchButton, controller) && canWindUp == true)      // When the Right Bumper is held down, execute
        {
            //Debug.Log("winding up a punch");
            windUpTimer += Time.deltaTime;              // increase the windUpTimer as time passes

            if (windUpTimer >= windUpMin)                // once you have wound up the punch enough...
            {
                //Debug.Log("You can punch");
                canPunch = true;                        // lets player fire a punch with the Punch() function
            }
        }
    }                                                       // the player can wind up a punch
    
    // after throwing a punch, there is a cooldown period where the player cannot throw a punch
    void WindUpCooldown()                                                   // after throwing a punch, there will be a cooldown before you can throw another punch
    {
        //Debug.Log("please wait before firing again");
        windUpTimer = 0.0f;
        //windUpTimer += Time.deltaTime;
        //if (windUpTimer >= windUpCD)
        //{
        //    canWindUp = true;
        //}
    }

    // launches the punch after the player has wound up a punch
    void Punch()                                                            // the player fires a punch
    {
        if (/*XCI.GetButtonUp(XboxButton.RightBumper*/XCI.GetButtonUp(punchButton, controller))                        // When the bumper is released, fire a punch
        {
            fist.enabled = true;
            if (canPunch == true)                                           // if the player can punch (have wound up enough)
            {
                //Debug.Log("Punch is firing");
                punchDuration = 0.0f;                                       // reset the punch duration timer
                canPunch = false;                                           // cannot punch now
                fistRB.AddForce(/*transform.parent.transform.forward*/ punchVector * punchSpeed);            // move the fist forward
                windUpTimer = 0.0f;                                         // reset how long you have to wind up a punch before you can fire
            }
            //Debug.Log("punch duration is ticking");
            canWindUp = false;                                              // make player unable to wind up another punch
            WindUpCooldown();                                               // invoke the WindUpCooldown() function
        }
        // how long the punch will go for
        punchDuration += Time.deltaTime;                                    // punch duration ticks up
        if (punchDuration >= punchDurationLimit)                            // once the punch has been out for long enough...
        {
            //Debug.Log("punch reset");
            transform.position = fistReset.transform.position;          // teleport the punch back to the reset point
            fistRB.velocity = Vector3.zero;                             // reset the velocity of the fist
            fistRB.angularVelocity = Vector3.zero;                      // reset the angular velocity of the fist
            fist.enabled = false;
        }
    }

    // other players will get knocked back if they get hit by a fist
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4") && other.tag != gameObject.transform.parent.transform.parent.transform.parent.tag)
        {
            //Debug.Log("enemy hit");
            other.GetComponent<Rigidbody>().AddForce(punchVector * knockback);
        }
    }

    // allows the player to aim their punch
    void AimPunch()
    {
        // get the X and Y inputs to aim the player punch around
        float aimAxisX = XCI.GetAxis(punchStickX, controller);              // generates a float based on the X movement of the punch stick
        float aimAxisY = XCI.GetAxis(punchStickY, controller);              // generates a float based on the Y movement of the punch stick
        //Debug.Log(aimAxisX);
        //Debug.Log(aimAxisY);

        punchVector = new Vector3(aimAxisX, 0, aimAxisY);           // generates a new Vector 3 based on the punch stick movement
        //Debug.DrawLine(transform.parent.transform.parent.transform.position, transform.parent.transform.parent.transform.position + punchVector.normalized * 10, aimDirectionColour); // draw line in editor
        //Debug.Log(punchVector);
        //Debug.Log(transform.parent.transform.parent.transform.position);
        //Debug.Log(punchVector.normalized);
        if (punchVector.magnitude < 0.1f)
        {
            punchVector = previousAimDirection;                             // if there is no input on the sticks, the punch Vector will default to the last location that was aimed
        }

        punchVector = punchVector.normalized;
        previousAimDirection = punchVector;
    }

    // draw a line from the body to the fists
    void UpdateLines()
    {
        punchLine.SetPosition(0, transform.parent.transform.position);
        punchLine.SetPosition(1, transform.position);

        // 1. get the line renderer component off of the game object punchLine
        // 2. set position Element 1 and Element 2 to the vectors for (shoulder, fist)
        // 3. give it a material that will stretch as it extends
    }

    void UpdateAimIndicator()
    {
        aimLine.SetPosition(0, aimIndicatorOrigin.transform.position);
        aimLine.SetPosition(1, aimIndicatorOrigin.transform.position + punchVector.normalized * 5);
    }
}
