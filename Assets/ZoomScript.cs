using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour {

    public Camera viewPort;

    public float normalFOV;
    public float zoomedFOV;
    bool isZoomed = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
        {
            isZoomed = !isZoomed;
            viewPort.fieldOfView = isZoomed ? zoomedFOV : normalFOV;
        }
	}
}
