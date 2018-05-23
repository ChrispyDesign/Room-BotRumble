using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerPunching : MonoBehaviour {

	// variables related to punching
	public float windUpMin;						// the amount of time needed before you can fire a punch
	public GameObject leftFist; 				// game object for left fist
	public GameObject rightFist;				// game object for right fist
	public float punchDistance;					// variable to determine how far a player can punch
	public float punchSpeed;					// variable to determine how fast the player's punch will move
	private bool canPunchLeft = true;			// bool to determine whether a player can throw a left punch
	private bool canPunchRight = true;			// determine whether player can throw a right punch

	private float timePassedLeft;				// amount of time that has passed, used to determine how long before you can fire a left punch again
	private float timePassedRight;				// amount of time that has passed, used to determine how long before you can fire a right punch again
	private float punchRate;					// amount of time after a punch before you can wind up another punch

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update () {
		Punch ();
	}

	private void Punch()
	{
		if (canPunchLeft)
		{
			if (XCI.GetButton(XboxButton.LeftBumper))
			{
				Debug.Log ("left bumper down");
				if (XCI.GetButtonUp (XboxButton.LeftBumper)) 
				{
					Debug.Log ("left bumper up");
					canPunchLeft = false;
//					leftFist.GetComponent<Rigidbody> ().AddForce (gameObject.transform.forward * punchSpeed, ForceMode.Force);		// after the button is pressed and released, fire a punch. Will need to change up this function
					PunchCooldownLeft;
				}

			}
		}

		if (canPunchRight) 
		{
			if (XCI.GetButton(XboxButton.RightBumper))
			{
				Debug.Log ("right bumper down");
				if (XCI.GetButtonUp (XboxButton.RightBumper)) 
				{
					Debug.Log ("right bumper up");
					canPunchRight = false;
//					rightFist.GetComponent<Rigidbody> ().AddForce (gameObject.transform.forward * punchSpeed, ForceMode.Force);		// after the button is pressed and released, fire a punch. Will need to change up this function
					PunchCooldownRight;
				}

			}
		}
	}

	// next two functions are for determining how much time after a punch it will be before you can throw another punch
	private void PunchCooldownLeft()
	{
		timePassedLeft = 0.0f;
		if (timePassedLeft >= punchRate) 
		{
			canPunchLeft = true;
		}
	}

	private void PunchCooldownRight()
	{
		timePassedLeft = 0.0f;
		if (timePassedRight >= punchRate) 
		{
			canPunchRight = true;
		}
	}

}
