using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootProjectile : NetworkBehaviour
{
    public GameObject shellPrefab;
    public Transform startingLocation;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        NetworkServer.Spawn(Instantiate(shellPrefab, startingLocation.position, startingLocation.rotation));
    }
}
