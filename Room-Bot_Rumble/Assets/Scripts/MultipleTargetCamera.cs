using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour {

    public List<Transform> targets;                                             // creates a list that looks for targets

    public Vector3 offset;                                                      // the offset of the camera

    private Vector3 velocity;                                                   // how fast the camera moves
    public float moveTime = 0.5f;                                               // how long it takes to reach the destination

    // how far out and in the camera can zoom
    public float minZoomDistance = 40.0f;
    public float maxZoomDistance = 10.0f;
    public float zoomLimiter = 50.0f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        // find all the players that are in the scene
        targets.Add(GameObject.Find("Player1").transform);
        targets.Add(GameObject.Find("Player2").transform);
        targets.Add(GameObject.Find("Player3").transform);
        targets.Add(GameObject.Find("Player4").transform);
    }


    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        MoveCamera();
        ZoomCamera();
    }

    // finds the centre point between all the players
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)                                                 // if there is only one player, it will just return their position
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);             // creates a bounds that encapsulates the players
        for (int i = 0; i < targets.Count; i++)                                 // adds all the other players to the bounds
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;                                                   // returns the vector3 of the centre of the bounds box
    }

    void MoveCamera()
    {
        Vector3 centerPoint = GetCenterPoint();                                 // every update, it looks for where the centre of the players are
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, moveTime);
    }

    void ZoomCamera()
    {
        float newZoom = Mathf.Lerp(maxZoomDistance, minZoomDistance, GetGreatestDistance()/zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }
}
