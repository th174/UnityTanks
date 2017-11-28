using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotTurret : MonoBehaviour
{
    public CameraController cameraController;
    public float rotateSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 lookVector = cameraController.CurrentCameraComponent.gameObject.transform.forward;
        Vector3 targetVector = Vector3.ProjectOnPlane(lookVector, this.transform.up);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetVector, this.transform.up), Time.deltaTime * rotateSpeed);
        this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0);
    }
}
