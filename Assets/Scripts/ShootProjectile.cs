using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootProjectile : NetworkBehaviour
{
    public float reloadTime;
    public GameObject shellPrefab;
    public Transform startingLocation;
	public AudioSource firingSource;

    private float reloadTimer;

    // Use this for initialization
    void Start()
    {
        reloadTimer = reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        reloadTimer += Time.deltaTime;
        if (reloadTimer >= reloadTime && Input.GetMouseButtonDown(0))
        {
            CmdFire();

			firingSource.Play ();
            reloadTimer = 0;
        }
    }

    [Command]
    void CmdFire()
    {
        NetworkServer.Spawn(Instantiate(shellPrefab, startingLocation.position, startingLocation.rotation));
    }
}
