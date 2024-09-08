using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    [SerializeField] float cameraYoffset = 2.0f;
    [SerializeField] float cameraZoffset = -5.0f;
    
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(0, cameraYoffset, cameraZoffset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
