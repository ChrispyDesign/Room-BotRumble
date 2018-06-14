using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerPunching : MonoBehaviour {

    // variables for doing multiple controllers
    public XboxController controller;                                           // allows you to hook up multiple controllers to different players
    public XboxButton punchButton;                                              // assigns the button that is used to control the punches
    public XboxAxis punchStickX;                                                // assigns the stick used to aim punches
    public XboxAxis punchStickY;                                                // assigns the stick used to aim punches

    // variable control the time and speed of punch
    private float punchDuration = 0.0f;                                         // timer that will tick with deltaTime, interacts with punch duration max
    public float punchDurationLimit = 0.5f;                                     // the amount of time that a punch will be travel for before it resets
    public float punchSpeed = 1000f;                                            // how fast the punch moves when it gets launched

    // winding up punches
    private float windUpTimer = 0.0f;                                           // how long the player has been winding up a punch
    public float windUpMin = 1.0f;                                              // amount of time you need to wind up a punch

    // cooldown after throwing a punch
    public bool canWindUp = true;                                               // whether player can wind up punch
    public float windUpCD = 1.0f;                                               // amount of time after a punch is thrown before you can wind up again

    // throwing a punch
    private bool canPunch = false;                                              // whether player can throw a punch after winding it up
    public Transform fistReset;                                                 // game object to reset the fist location
    public Rigidbody fistRB;                                                    // get the fist's rigidbody
    public SphereCollider fist;                                                 // grabs the sphere collider for the fist

    // aiming a punch
    public Vector3 previousAimDirection = Vector3.forward;                      // initialises the aiming of the punch to directly in front of you
    public Transform aimIndicatorOrigin;                                        // where the aim indicators will originate from
    public LineRenderer aimLine;                                                // the line renderer for the aim indicator
    private Vector3 punchVector;                                                // where a punch is being aimed

    // knocking back a player with a punch
    public float knockback = 2000f;

    // drawing a line from the body to the fists
    public LineRenderer punchLine;                                              // the line renderer for the fists
    public GameObject punchLight;                                               // the light attached to the fists

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start () {
        fistRB = GetComponent<Rigidbody>();                                     // assign the variable for the fist rigid body
        fist.enabled = false;                                                   // the fist starts off as inactive, so that you can't punch people without actually throwing a punch
	}

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Update ()
    {
        WindUp();
        Punch();
        windUpTimer += Time.deltaTime;                                          // ticks up the Wind Up Timer
        if (windUpTimer >= windUpCD)                                            // if punch wind up is off cooldown, allows the player to wind up a punch
        {
            canWindUp = true;
        }
        AimPunch();
        UpdateLines();
        UpdateAimIndicator();
        FlashPunchIndicator();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // allow the player to wind up a punch before they can fire it
    void WindUp()
    {
        if (XCI.GetButton(punchButton, controller) && canWindUp == true)        // When the Right Bumper is held down...
        {
            windUpTimer += Time.deltaTime;                                      // increase the windUpTimer as time passes

            if (windUpTimer >= windUpMin)                                       // once you have wound up the punch enough...
            {
                canPunch = true;                                                // lets player fire a punch with the Punch() function
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // after throwing a punch, there is a cooldown period where the player cannot throw a punch
    void WindUpCooldown()                                                       // after throwing a punch, there will be a cooldown before you can throw another punch
    {
        windUpTimer = 0.0f;                                                     // sets that timer to 0. It is constantly increasing in the Update() function
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // launches the punch after the player has wound up a punch
    void Punch()
    {
        if (XCI.GetButtonUp(punchButton, controller))                           // when the bumper is released...
        {
            fist.enabled = true;                                                // enables the fist so it can start colliding with things
            if (canPunch == true)                                               // if the player can punch (have wound up enough)...
            {
                punchDuration = 0.0f;                                           // reset the punch duration timer
                canPunch = false;                                               // the player can no longer punch
                fistRB.AddForce(punchVector * punchSpeed);                      // apply force to the fist, to move it forward
                windUpTimer = 0.0f;                                             // reset how long you have to wind up a punch before you can fire
            }
            canWindUp = false;                                                  // make player unable to wind up another punch
            WindUpCooldown();                                                   // invoke the WindUpCooldown() function
        }
        // how long the punch will go for
        punchDuration += Time.deltaTime;                                        // punch duration ticks up
        if (punchDuration >= punchDurationLimit)                                // once the punch has been out for long enough...
        {
            transform.position = fistReset.transform.position;                  // teleport the punch back to the reset point
            fistRB.velocity = Vector3.zero;                                     // reset the velocity of the fist
            fistRB.angularVelocity = Vector3.zero;                              // reset the angular velocity of the fist
            fist.enabled = false;
            punchLight.SetActive(false);                                        // turns off the light attached to the fists
        }
    }

    // other players will get knocked back if they get hit by a fist
    private void OnTriggerEnter(Collider other)
    {
        // if the fist collides with a player that isn't the player that this fist is attached to
        if ((other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4") && other.tag != gameObject.transform.parent.transform.parent.transform.parent.tag)
        {
            other.GetComponent<Rigidbody>().AddForce(punchVector * knockback);  // knock back the player that the punch hit
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // draw a line from the body to the fists
    void UpdateLines()
    {
        punchLine.SetPosition(0, transform.parent.transform.position);      // sets the beginning of the punch line line renderer to be the fist reset position
        punchLine.SetPosition(1, transform.position);                       // sets the end of the punch line line renderer to be where the fist is
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // lets the player rotate the aim indicator, showing where there punch will land
    void UpdateAimIndicator()
    {
        aimLine.SetPosition(0, aimIndicatorOrigin.transform.position);      // sets the beginning of the aim indicator line renderer to be where the player is
        aimLine.SetPosition(1, aimIndicatorOrigin.transform.position + punchVector.normalized * 5);     // sets the end of the aim indicator line renderer to be where the player is tilting the sticks, normalised
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // will turn a light on while the player's punch is charged
    void FlashPunchIndicator()
    {
        if (canPunch == true)
            punchLight.SetActive(true);
    }
}
