using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetToFollow;
    public float smoothSpeed = 0.125f;
    public Vector3 cameraOffset = new Vector3(0.0f, 15.0f, -0.25f);

    private void FixedUpdate()
    {
        Vector3 desiredPosition = targetToFollow.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition; 
    }
}
