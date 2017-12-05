using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MoveScript : NetworkBehaviour
{
    public Track leftTrack;
    public Track rightTrack;
    public float forwardMotorTorque;
    public float reverseMotorTorque;
    public float brakeTorque;

    private float leftTorque;
    private float rightTorque;
    private float leftBrake;
    private float rightBrake;

    public AudioSource movementSource;

    public GUIStyle speedLabelStyle;

    private void OnGUI()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GUI.Label(new Rect(Screen.width - 100, 10, 90, 20), string.Format("Speed: {0:F0}km/h", this.gameObject.GetComponentInChildren<Rigidbody>().velocity.magnitude * 3.6), speedLabelStyle);
    }

    public void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        leftTorque = Input.GetKey(KeyCode.Q) ? forwardMotorTorque : Input.GetKey(KeyCode.A) ? reverseMotorTorque : 0;
        rightTorque = Input.GetKey(KeyCode.E) ? forwardMotorTorque : Input.GetKey(KeyCode.D) ? reverseMotorTorque : 0;
        leftBrake = !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.A) ? brakeTorque : 0;
        rightBrake = !Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.D) ? brakeTorque : 0;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
        {
            movementSource.volume = 1;
        }
        else
        {
            movementSource.volume = 0;
        }
    }

    public void FixedUpdate()
    {
        leftTrack.MotorTorque = leftTorque;
        rightTrack.MotorTorque = rightTorque;
        leftTrack.BrakeTorque = leftBrake;
        rightTrack.BrakeTorque = rightBrake;

    }
}

[System.Serializable]
public class Track
{
    public float MotorTorque
    {
        get
        {
            return wheels[0].motorTorque;
        }
        set
        {
            wheels.ForEach(wheel => wheel.motorTorque = value);
        }
    }
    public float BrakeTorque
    {
        get
        {
            return wheels[0].brakeTorque;
        }
        set
        {
            wheels.ForEach(wheel => wheel.brakeTorque = value);
        }
    }
    public List<WheelCollider> wheels;
}