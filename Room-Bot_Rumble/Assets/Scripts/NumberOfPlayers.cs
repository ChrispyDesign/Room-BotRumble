using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfPlayers : MonoBehaviour {

    // List that will contain data on the number of players who are playing
    public List<bool> PlayersPlaying;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayersPlaying.Add(false);
        PlayersPlaying.Add(false);
        PlayersPlaying.Add(false);
        PlayersPlaying.Add(false);
    }
}
