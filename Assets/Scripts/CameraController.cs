using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Targets")]
    public Transform player; // The player transform

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement
    public float followDistance = 2f; // Minimum distance from the player
    public float maxDistanceFromPlayer = 2.5f; // Maximum distance from the player

    void LateUpdate()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure we're on the correct plane

        // Calculate the desired position based on the mouse position
        Vector3 desiredPosition = mousePosition;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(player.position, desiredPosition);

        // Clamp the desired position based on the distance
        if (distanceToPlayer < followDistance)
        {
            desiredPosition = player.position + (desiredPosition - player.position).normalized * followDistance;
        }
        else if (distanceToPlayer > maxDistanceFromPlayer)
        {
            desiredPosition = player.position + (desiredPosition - player.position).normalized * maxDistanceFromPlayer;
        }

        // Maintain the camera's original Z position
        desiredPosition.z = transform.position.z;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
