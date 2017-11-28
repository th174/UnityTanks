using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour
{

    public Camera[] cameras;
    public SmoothMouseLook[] mouseLooks;
    public Transform baseRotation;
    private int index = 0;

    public Camera CurrentCameraComponent
    {
        get
        {
            return cameras[index];
        }
    }

    public SmoothMouseLook CurrentMouseLookComponent
    {
        get
        {
            return mouseLooks[index];
        }
    }

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            CurrentCameraComponent.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            CurrentCameraComponent.enabled = false;
            index = (index + 1) % cameras.Length;
            CurrentCameraComponent.enabled = true;
            CurrentMouseLookComponent.ResetRotation(baseRotation.rotation);
        }
        //Debug.DrawRay(CurrentCameraComponent.gameObject.transform.position, CurrentCameraComponent.gameObject.transform.forward.normalized * 100, Color.red, 10, false);
    }
}
