using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void UpdateAimTransform()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure mouse position is in 2D plane

        // Calculate the direction and distance from the player to the mouse
        Vector3 direction = (mousePosition - playerTransform.position).normalized;
        float distance = Vector3.Distance(playerTransform.position, mousePosition);

        // Clamp the distance between the minimum and maximum allowed values
        distance = Mathf.Clamp(distance, minAimDistance, maxAimDistance);

        // Update the aimTransform position to follow the mouse within the clamped range
        aimTransform.position = playerTransform.position + direction * distance;
    }

    private void FaceTargetDirection()
    {
        // Compare the x positions of aimTransform and playerTransform
        if (aimTransform.position.x > playerTransform.position.x)
        {
            // Aim is to the right of the player, face right
            playerTransform.localScale = new Vector3(xScale, playerTransform.localScale.y, playerTransform.localScale.z);
        }
        else if (aimTransform.position.x < playerTransform.position.x)
        {
            // Aim is to the left of the player, face left
            playerTransform.localScale = new Vector3(-xScale, playerTransform.localScale.y, playerTransform.localScale.z);
        }
    }
}
