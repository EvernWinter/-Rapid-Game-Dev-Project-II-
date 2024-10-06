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
    
    [Header("Background")]
    public BoxCollider2D backgroundCollider;
    
    private float camHalfHeight, camHalfWidth;
    private Vector2 minBounds, maxBounds;

    
    void Start()
    {
        // Ensure the camera is in orthographic mode
        Camera cam = Camera.main;
        if (!cam.orthographic)
        {
            Debug.LogError("Camera must be in orthographic mode for this script to work properly.");
            return;
        }

        // Get the camera dimensions
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;

        // Get the bounds of the background using the BoxCollider2D
        if (backgroundCollider != null)
        {
            Bounds bounds = backgroundCollider.bounds;

            // Calculate the minimum and maximum bounds for the camera
            minBounds = new Vector2(bounds.min.x + camHalfWidth, bounds.min.y + camHalfHeight);
            maxBounds = new Vector2(bounds.max.x - camHalfWidth, bounds.max.y - camHalfHeight);
        }
        else
        {
            Debug.LogError("No BoxCollider2D assigned for the background!");
        }
    }
    
    
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
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Clamp the camera position to stay within the background bounds
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

        transform.position = smoothedPosition;
    }
}
