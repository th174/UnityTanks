using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public float maxHealth;
    public GameObject deathEffect;
    private string restartText = "Request Rematch";
    public GUIStyle gameOverLabelStyle;
    public GUIStyle healthLabelStyle;
    public GUIStyle buttonTextStyle;

    internal bool isDead;
    internal bool gameOver;

    [SyncVar]
    private float currentHealth;
    [SyncVar]
    private int rematchVote;
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        deathEffect.SetActive(false);
        if (isLocalPlayer)
        {
            this.gameObject.GetComponentInChildren<AudioListener>().enabled = true;
        }
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GUI.Label(new Rect(10, Screen.height - 30, 200, 20), string.Format("Hull integrity: {0:F0}%", currentHealth), healthLabelStyle);
        if (gameOver)
        {
            if(GUI.Button(new Rect(Screen.width / 2 - 100, 200, 200, 70), restartText))
            {
                restartText = "Rematch Requested...";
                Destroy(this.gameObject);
                rematchVote++;
                if (rematchVote >= FindObjectsOfType<PlayerController>().Length)
                {
                    Reset();
                }
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

    public void TakeDamage(float amount)
    {
        if (!isServer)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            this.RpcDeath();
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
            this.GetComponent<DrawCrosshairs>().enabled = false;
            this.GetComponent<TurretController>().enabled = false;
            this.GetComponent<MoveScript>().enabled = false;
            this.GetComponent<ShootProjectile>().enabled = false;
        }
    }

    void Reset()
    {
        var myIsHost = this.isServer;
        NetworkManager.singleton.StopHost();
        NetworkServer.Reset();
        if (myIsHost)
        {
            NetworkManager.singleton.StartHost();
        } else
        {
            NetworkManager.singleton.StartClient();
        }
    }
}
