using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfPlayers : MonoBehaviour {

    // List that will contain data on the number of players who are playing
    public List<bool> PlayersPlaying;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // makes this object not get destroyed when we load a new scene, so other scripts can reference this
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // adds 4 bool elements to the list. These elements are manipulated with other scripts
    private void Start()
    {
        PlayersPlaying.Add(false);
        PlayersPlaying.Add(false);
        PlayersPlaying.Add(false);
        PlayersPlaying.Add(false);
    }
}
