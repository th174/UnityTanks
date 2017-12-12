using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour
{

    public GameObject deathEffect;
    private string restartText = "Request Rematch";
    public GUIStyle gameOverLabelStyle;
    public GUIStyle buttonTextStyle;
    public short playerId;

    internal bool isDead;
    internal bool gameOver;

    private static int rematchVote;
    private static int connectedPlayers;
    // Use this for initialization
    void Start()
    {
        deathEffect.SetActive(false);
        if (isServer)
        {
            PlayerController.connectedPlayers++;
        }
        if (isLocalPlayer)
        {
            this.gameObject.GetComponentInChildren<AudioListener>().enabled = true;
            playerId = this.GetComponent<NetworkIdentity>().playerControllerId;
        }
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        if (Network.isClient)
        {
            Exit();
        }
    }

    void Update()
    {
        if (isServer)
        {
            var players = FindObjectsOfType<PlayerController>();
            var remainingPlayers = 0;
            foreach (var player in players)
            {
                remainingPlayers += player.isDead ? 0 : 1;
            }
            if (connectedPlayers > remainingPlayers && remainingPlayers <= 1)
            {
                Array.ForEach<PlayerController>(players, p => p.RpcEndGame());
            }
        }
    }

    [Command]
    void CmdForceDeath()
    {
        RpcDeath();
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (gameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            if (GUI.Button(new Rect(Screen.width / 2 - 205, 200, 200, 70), restartText))
            {
                if (!restartText.Contains("..."))
                {
                    CmdRestart();
                }
                restartText = "Rematch Requested...";
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 5, 200, 200, 70), "Quit to Main Menu"))
            {
                Exit();
            }
        }
        if (isDead)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200, 50, 400, 300), "Game Over", gameOverLabelStyle);
        }
        if (gameOver && !isDead)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200, 100, 400, 300), "Congratulations, you win!", gameOverLabelStyle);
        }
    }

    [ClientRpc]
    public void RpcDeath()
    {
        deathEffect.SetActive(true);
        deathEffect.GetComponent<ParticleSystem>().Play();
        if (isLocalPlayer)
        {
            this.GetComponent<DrawCrosshairs>().enabled = false;
            this.GetComponent<TurretController>().enabled = false;
            this.GetComponent<MoveScript>().enabled = false;
            this.GetComponent<ShootProjectile>().enabled = false;
        }
        isDead = true;
    }

    [ClientRpc]
    public void RpcEndGame()
    {
        gameOver = true;
        if (isLocalPlayer)
        {
            this.GetComponent<MoveScript>().forwardMotorTorque = 0;
            this.GetComponent<MoveScript>().reverseMotorTorque = 0;
            this.GetComponent<MoveScript>().FixedUpdate();
            this.GetComponent<DrawCrosshairs>().enabled = false;
            this.GetComponent<TurretController>().enabled = false;
            this.GetComponent<MoveScript>().enabled = false;
            this.GetComponent<ShootProjectile>().enabled = false;
        }
    }

    [Command]
    void CmdRestart()
    {
        PlayerController.rematchVote++;
        if (rematchVote >= FindObjectsOfType<PlayerController>().Length)
        {
            PlayerController.rematchVote = 0;
            PlayerController.connectedPlayers = 0;
            RpcRestart();
        }
    }

    [ClientRpc]
    void RpcRestart()
    {
        ClientScene.RemovePlayer(playerId);
        ClientScene.AddPlayer(playerId);
    }


    void Exit()
    {
        PlayerController.connectedPlayers = 0;
        PlayerController.rematchVote = 0;
        NetworkManager.singleton.StopHost();
        NetworkServer.Shutdown();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
