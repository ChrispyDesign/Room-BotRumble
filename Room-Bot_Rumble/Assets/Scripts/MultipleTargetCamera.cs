using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour {

    public List<GameObject> targets;

    private void Start()
    {
        targets.Add(GameObject.Find("Player 1"));
        targets.Add(GameObject.Find("Player 2"));
        targets.Add(GameObject.Find("Player 3"));
        targets.Add(GameObject.Find("Player 4"));
    }

    private void LateUpdate()
    {
        
    }

}
