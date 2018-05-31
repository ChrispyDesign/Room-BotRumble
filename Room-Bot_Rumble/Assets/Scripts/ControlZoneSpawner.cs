using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlZoneSpawner : MonoBehaviour {

    public BoxCollider BC;          // box collider
    private float minX;             // variables for the dimensions of the spawner
    private float minZ;
    private float maxX;
    private float maxZ;

    public GameObject controlZonePrefab;
    public bool canSpawn = true;    // whether the spawner can spawn a control zone
    private Vector3 CZSpawnPos;     // the position where the CZ will spawn

    void Start () {
        BC = GetComponent<BoxCollider>();                       // Assign BC to the box collider of the spawner
        // assign the variables for the bounds of the box collider
        minX = BC.bounds.min.x;
        minZ = BC.bounds.min.z;
        maxX = BC.bounds.max.x;
        maxZ = BC.bounds.max.z;
    }
	
	// Update is called once per frame
	void Update () {
		if (canSpawn)
        {
            CZSpawnPos = new Vector3(Random.Range(minX, maxX), 0.0f, Random.Range(minZ, maxZ));
            Instantiate(controlZonePrefab, CZSpawnPos, transform.rotation);
            canSpawn = false;
        }
	}
}
