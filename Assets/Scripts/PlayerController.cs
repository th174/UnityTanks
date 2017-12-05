using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public float maxHealth;
    private Rect healthBarLocation = new Rect(10, Screen.height - 30, 200, 200);
    public GameObject deathEffect;
    public GameController game;

    internal bool isDead;
    internal bool gameOver;

    [SyncVar]
    private float currentHealth;
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        deathEffect.SetActive(false);
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GUI.Label(healthBarLocation, string.Format("Hull integrity:{0}%", currentHealth));
        if (gameOver)
        {
            GUI.Button(new Rect(Screen.width / 2 - 200, 300, 400, 300), "Restart?");
        }
        if (isDead)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200, 0, 400, 300), "Game Over");
        }
        if (gameOver && !isDead)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200, 0, 400, 300), "Congratulations, you win!");
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
}
