using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{

    public float maxHealth;
    private Rect healthBarLocation = new Rect(10, Screen.height - 30, 200, 200);
    public GameObject deathEffect;

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
        if (isLocalPlayer)
        {
            GUI.Label(healthBarLocation, string.Format("Hull integrity:{0}%", currentHealth));
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
    }
}
