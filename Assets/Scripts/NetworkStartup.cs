using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkStartup : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        if (SceneParameters.isHost)
        {
            NetworkManager.singleton.StartHost();
        }
        else
        {
            NetworkManager.singleton.StartClient();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
