using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    // setting up multiple controllers
    public Rigidbody playerRB;
    public XboxController controller;
    
    // defining variables for the player movespeed
    public float moveSpeed = 3.0f;
    public float rotationSpeed = 75.0f;
    public float maxSpeed = 5.0f;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        MovePlayer();
        RotatePlayer();
	}

    void MovePlayer()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;         // just adjust the transform to move the player

        //Debug.Log("I'm moving");

        //if (Vector3.Magnitude(playerRB.velocity)<=maxSpeed)                                 // only add force if the player is slowing down
        //{
        //    playerRB.AddForce(transform.forward * moveSpeed);                               // use physics on a rigid body to move the player
        //    Debug.DrawLine(playerRB.position, Vector3.forward, Color.black);
        //}

    }

    // Rotate the player when one of the triggers is held down
    void RotatePlayer()
    {
        if (XCI.GetButton(XboxButton.LeftBumper, controller))
        {
            //Debug.Log("Left Bumper");
            transform.Rotate(new Vector3(0.0f, -rotationSpeed * Time.deltaTime));
        }
        if (XCI.GetButton(XboxButton.RightBumper, controller))
        {
            //Debug.Log("Right Bumper");
            transform.Rotate(new Vector3(0.0f, rotationSpeed * Time.deltaTime));
        }
    }

    
}
