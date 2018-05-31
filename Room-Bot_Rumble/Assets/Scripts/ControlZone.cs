using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlZone : MonoBehaviour {

    public float maxPoints = 10;                    // the amount of points that a control zone will spawn with
    public float currentPoints = 1;                 // the total amount of points that this control zone has. When this runs out, the zone will disappear and reappear
    public GameObject CZSpawner;                    // stores the spawner gameobject
    private float despawnTimer = 0.0f;              // timer for when the CZ will despawn
    public float despawnMax = 0.25f;                // time before the CZ will despawn

    private void Start()
    {
        currentPoints = maxPoints;
        CZSpawner = GameObject.Find("ControlZoneSpawner");
    }

    private void Update()
    {
        currentPoints -= Time.deltaTime;
        if (currentPoints <= 0)
        {
            //transform.position = new Vector3(0, -10, 0);
            GetComponent<Rigidbody>().AddForce(0, -100, 0);
            despawnTimer += Time.deltaTime;
        }

        if (despawnTimer >= despawnMax)
        {
            despawnTimer = 0.0f;
            CZSpawner.GetComponent<ControlZoneSpawner>().canSpawn = true;
            Destroy(gameObject);
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player 1")
    //    {
    //        GetComponent<PlayerPointCounter>()
    //    }
    //}

}
