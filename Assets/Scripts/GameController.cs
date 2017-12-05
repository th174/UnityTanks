using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{
    private int deadPlayers;
    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (!isServer || timeElapsed < .25)
        {
            return;
        }
        timeElapsed = 0;
        var players = FindObjectsOfType<PlayerController>();
        var deadPlayers = 0;
        foreach (var player in players)
        {
            deadPlayers += player.isDead ? 1 : 0;
        }
        if (players.Length - deadPlayers <= 1 && deadPlayers > 0)
        {
            Array.ForEach<PlayerController>(players, p => p.RpcEndGame());
        }
    }
}
