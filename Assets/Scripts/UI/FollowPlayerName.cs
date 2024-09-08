using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowPlayerName : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    void Update()
    {
        Vector3 worldPosition = player.position + offset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        transform.position = Vector3.Lerp(transform.position, screenPosition, smoothSpeed);
    }
}
