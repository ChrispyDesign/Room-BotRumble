using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlZone : MonoBehaviour {

    public float maxPoints = 10;                    // the amount of points that a control zone will spawn with
    public float currentPoints = 1;                 // the total amount of points that this control zone has. When this runs out, the zone will disappear and reappear
    public GameObject CZSpawner;                    // stores the spawner gameobject
    private float despawnTimer = 0.0f;              // timer for when the CZ will despawn
    public float despawnMax = 0.25f;                // time before the CZ will despawn

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Start()
    {
        currentPoints = maxPoints;                                          // sets the current point pool of the control zone to be the maximum amount of allocatable points
        CZSpawner = GameObject.Find("ControlZoneSpawner");                  // finds the CZ spawner game object
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        currentPoints -= Time.deltaTime;                                    // as time ticks, the current points will constantly decrease
        if (currentPoints <= 0)                                             // once the current points reaches zero...
        {
            GetComponent<Rigidbody>().AddForce(0, -100, 0);                 // moves the CZ downwards, which will trigger the OnTriggerExit functions in the PlayerMovement scripts
            despawnTimer += Time.deltaTime;                                 // despawn timer begins ticking up
        }

        if (despawnTimer >= despawnMax)                                     // once the despawn timer reaches the despawnMax (cooldown between a CZ being spawned)
        {
            despawnTimer = 0.0f;                                            // resets the despawn timer
            CZSpawner.GetComponent<ControlZoneSpawner>().canSpawn = true;   // canSpawn = true, letting the CZ spawner spawn a new CZ
            Destroy(gameObject);                                            // destroy the current CZ
        }
    }
}
