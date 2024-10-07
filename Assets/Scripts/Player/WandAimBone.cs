using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAimBone : MonoBehaviour
{
    [Header("Aim Settings")]
    [SerializeField] private Transform playerTransform;   // Reference to the player's transform
    [SerializeField] private Transform aimTransform;      // Reference to the aiming object (e.g., wand)
    [SerializeField] private float minAimDistance = 1f;   // Minimum distance the aim can be from the player
    [SerializeField] private float maxAimDistance = 5f;   // Maximum distance the aim can be from the player
    
    private Vector3 initialPlayerScale;                   // Store initial player scale for flipping purposes

    // Enum to track the direction the player is facing
    public enum FacingDirection
    {
        Left,
        Right
    }

    private FacingDirection currentFacingDirection;       // Track current facing direction

    private void Start()
    {
        // Store the player's initial scale to handle flipping
        initialPlayerScale = playerTransform.localScale;
    }

    private void Update()
    {
        // Update the aim position and face the correct direction
        UpdateAimTransform();
        FaceTargetDirection();
        
        // Add this in FaceTargetDirection() to debug the values
        Debug.Log($"Aim Position X: {aimTransform.position.x}, Player Position X: {playerTransform.position.x}");

    }

    // Update the aiming transform based on the mouse position and clamp it within a certain distance
    private void UpdateAimTransform()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Keep the mouse position on the 2D plane

        // Calculate the direction from the player to the mouse
        Vector3 direction = (mousePosition - playerTransform.position).normalized;

        // Calculate the distance from the player to the mouse
        float distance = Vector3.Distance(playerTransform.position, mousePosition);

        // Clamp the distance within the allowed minimum and maximum
        distance = Mathf.Clamp(distance, minAimDistance, maxAimDistance);

        // Update the aimTransform position based on the direction and clamped distance
        aimTransform.position = playerTransform.position + direction * distance;
    }

    // Flip the player to face the target direction based on the aim position
    private void FaceTargetDirection()
    {
        // Compare the x positions of the aimTransform and playerTransform
        if (aimTransform.position.x > playerTransform.position.x)
        {
            playerTransform.localScale = new Vector3(Mathf.Abs(initialPlayerScale.x), initialPlayerScale.y, initialPlayerScale.z);
            currentFacingDirection = FacingDirection.Right; // Update direction
        }
        else if (aimTransform.position.x < playerTransform.position.x)
        {
            // Aim is to the left of the player, face left (flip horizontally)
            playerTransform.localScale = new Vector3(-Mathf.Abs(initialPlayerScale.x), initialPlayerScale.y, initialPlayerScale.z);
            currentFacingDirection = FacingDirection.Left; // Update direction
        }
    }
}
