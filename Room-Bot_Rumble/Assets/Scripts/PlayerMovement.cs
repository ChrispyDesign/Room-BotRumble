using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    // setting up multiple controllers for each player
    public Rigidbody playerRB;                                              // stores the rigid body of the player
    public XboxController controller;                                       // lets you assign which controller to each player
    
    // defining variables for the player movespeed
    public float moveSpeed = 3.0f;                                          // how fast the player can move
    public float rotationSpeed = 75.0f;                                     // how fast they can rotate

    // game objects for where fists are so we can invoke their functions
    public GameObject leftFist; 
    public GameObject rightFist;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();                               // get the rigid body of the player
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void FixedUpdate () {
        MovePlayer();
        RotatePlayer();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // the player will constantly be moving forward
    void MovePlayer()
    {
        playerRB.MovePosition(playerRB.position + transform.forward * moveSpeed * Time.deltaTime);              // moves the player forward using physics on update
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Rotate the player when one of the bumpers is held down
    void RotatePlayer()
    {
        // if both bumpers are held down, you do not turn and just continue moving straight
        if (XCI.GetButton(XboxButton.LeftBumper, controller) && XCI.GetButton(XboxButton.RightBumper, controller))
        {
            return;
        }

        // if the left bumper is held down, rotate the player to the left based on the Rotation Speed
        if (XCI.GetButton(XboxButton.LeftBumper, controller))
        {
            playerRB.MoveRotation(playerRB.transform.rotation * Quaternion.Euler(0.0f, -rotationSpeed * Time.deltaTime, 0.0f));
        }
        // if the right bumper is held down, rotate the player to the left based on the Rotation Speed
        if (XCI.GetButton(XboxButton.RightBumper, controller))
        {
            playerRB.MoveRotation(playerRB.transform.rotation * Quaternion.Euler(0.0f, rotationSpeed*Time.deltaTime, 0.0f));
        }
    }
}
