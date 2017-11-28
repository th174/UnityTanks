using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotGun : MonoBehaviour
{

    public CameraController cameraController;
    public float rotateSpeed;
    public float maxDepression;
    public float maxElevation;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 lookVector = cameraController.CurrentCameraComponent.gameObject.transform.forward;
        Vector3 targetVector = Vector3.ProjectOnPlane(lookVector, this.transform.right);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetVector, this.transform.up), Time.deltaTime * rotateSpeed);
        this.transform.localEulerAngles = new Vector3(Mathf.Clamp(this.transform.localEulerAngles.x - (this.transform.localEulerAngles.x > 180 ? 360 : 0), -maxElevation, -maxDepression), 0, 0);
    }
}
