using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: CameraFollow.cs
*/

public class CameraFollow : MonoBehaviour    // Behavior for the following camera
{
    public Transform target;
    public float smoothSpeed = 0.125f;       // Includes a slight delay in following to create smoother motion
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;                                             // set camera's position to the target's (Player bee model), with an offset so that it is not inside it
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);      // a method I found to slightly delay the camera following, so that the bee's bobbing was still visible
        transform.position = smoothedPosition;                                                          // set camera's position to slightly delayed player bee's position
    }
}
