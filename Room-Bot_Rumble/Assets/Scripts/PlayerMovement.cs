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
    //public float maxSpeed = 5.0f;

    // game objects for where fists are so we can invoke their functions
    public GameObject leftFist;
    public GameObject rightFist;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();           // get the rigid body of the player
    }

    void FixedUpdate () {
        MovePlayer();           // run Move Player()
        RotatePlayer();         // run Rotate Player()
        //Debug.DrawLine(transform.position, leftFist.transform.position, Color.blue);    // draw a line from the body to the left fist
        //Debug.DrawLine(transform.position, rightFist.transform.position, Color.red);    // draw a line from the body to the right fist
    }

    // the player will constantly be moving forward
    void MovePlayer()
    {
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;         // just adjust the transform to move the player

        //Debug.Log("I'm moving");

        //if (Vector3.Magnitude(playerRB.velocity) <= maxSpeed)                                 // only add force if the player is slowing down
        //{
        //    playerRB.AddForce(transform.forward * moveSpeed);                               // use physics on a rigid body to move the player
        //    Debug.DrawLine(playerRB.position, Vector3.forward, Color.black);
        //}

        playerRB.MovePosition(playerRB.position + transform.forward * moveSpeed * Time.deltaTime);

    }

    // Rotate the player when one of the triggers is held down
    void RotatePlayer()
    {
        if (XCI.GetButton(XboxButton.LeftBumper, controller) && leftFist.GetComponent<PlayerPunching>().canWindUp == true && XCI.GetButton(XboxButton.RightBumper, controller) && rightFist.GetComponent<PlayerPunching>().canWindUp == true)
        {
            return;
        }
        if (XCI.GetButton(XboxButton.LeftBumper, controller)/* && leftFist.GetComponent<PlayerPunching>().canWindUp == true*/)      // also checks if the player can wind up a punch on the respective side
        {
            //Debug.Log("Left Bumper");
            //transform.Rotate(new Vector3(0.0f, -rotationSpeed * Time.deltaTime));
            playerRB.MoveRotation(playerRB.transform.rotation * Quaternion.Euler(0.0f, -rotationSpeed * Time.deltaTime, 0.0f));
            //playerRB.MoveRotation(new Quaternion(0.0f, -rotationSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        if (XCI.GetButton(XboxButton.RightBumper, controller)/* && rightFist.GetComponent<PlayerPunching>().canWindUp == true*/)    // also checks if the player can wind up a punch on the respective side
        {
            //Debug.Log("Right Bumper");
            //transform.Rotate(new Vector3(0.0f, rotationSpeed * Time.deltaTime));
            playerRB.MoveRotation(playerRB.transform.rotation * Quaternion.Euler(0.0f, rotationSpeed*Time.deltaTime, 0.0f));
        }
    }

    
}
