using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DrawCrosshairs : NetworkBehaviour
{
    public Texture noHitCrossHair;
    public Texture hitCrossHair;
    public CameraController cameraController;
    public float size;
    public float distance;
    public Transform startPoint;
    Texture crosshairSprite;
    Vector2 coordinates;
    RaycastHit hit;

    private void Start()
    {
        crosshairSprite = noHitCrossHair;
    }

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUI.DrawTexture(new Rect(0, 0, size, size)
            {
                center = new Vector2(coordinates.x, cameraController.CurrentCameraComponent.pixelHeight - coordinates.y)
            },
            crosshairSprite);
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Physics.Raycast(startPoint.position, startPoint.forward, out hit, distance))
        {
            coordinates = (Vector2) cameraController.CurrentCameraComponent.WorldToScreenPoint(hit.point);
            crosshairSprite = hitCrossHair;
        }
        else
        {
            coordinates = (Vector2) cameraController.CurrentCameraComponent.WorldToScreenPoint(startPoint.position + (startPoint.forward.normalized * distance));
            crosshairSprite = noHitCrossHair;
        }
        //Debug.DrawRay(startPoint.position, startPoint.forward.normalized * distance, Color.red, 1, true);
    }
}
