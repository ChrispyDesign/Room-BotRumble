using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlZoneSpawner : MonoBehaviour {

    public BoxCollider BC;                                          // the box collider, inside the bounds of which the CZ will be spawned

    // variables for the dimensions of the spawner, used to construct a bounds for the spawner
    private float minX;             
    private float minZ;
    private float maxX;
    private float maxZ;

    public GameObject controlZonePrefab;                            // stores the CZ prefab that will be spawned
    public bool canSpawn = true;                                    // whether the spawner can spawn a control zone
    private Vector3 CZSpawnPos;                                     // the position where the CZ will spawn

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start () {
        BC = GetComponent<BoxCollider>();                           // Assign BC to the box collider of the spawner
        
        // assign the variables for the bounds of the box collider
        minX = BC.bounds.min.x;
        minZ = BC.bounds.min.z;
        maxX = BC.bounds.max.x;
        maxZ = BC.bounds.max.z;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Update () {
		if (canSpawn)                                                                                       // if you can spawn...
        {
            CZSpawnPos = new Vector3(Random.Range(minX, maxX), 0.0f, Random.Range(minZ, maxZ));             // sets the spawn position to be a random point inside the bounds
            Instantiate(controlZonePrefab, CZSpawnPos, transform.rotation);                                 // instantiates the CZ prefab at this new random point
            canSpawn = false;                                                                               // stops the spawner from spawning a new CZ
        }
	}
}
