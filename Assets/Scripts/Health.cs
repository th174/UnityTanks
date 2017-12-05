using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public float maxHealth;
    public GUIStyle healthLabelStyle;

    [SyncVar]
    private float currentHealth;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update () {
		
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
            this.GetComponent<PlayerController>().RpcDeath();
        }
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GUI.Label(new Rect(10, 10, 200, 20), string.Format("Hull integrity: {0:F0}%", currentHealth), healthLabelStyle);
    }
}
