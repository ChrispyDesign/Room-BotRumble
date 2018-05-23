using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    // defining variables for the player movespeed
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;

    private Rigidbody playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    void Update () {
        MovePlayer();
        RotatePlayer();
	}

    void MovePlayer()
    {   
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // Rotate the player when one of the triggers is held down
    void RotatePlayer()
    {
        if (XCI.GetButton(XboxButton.LeftBumper))
        {
            Debug.Log("Left Bumper");
        }
    }

    
}
