using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Any update logic if necessary
    }

    // Trigger detection (when using a Trigger Collider)
    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet hit: " + other.tag); // Log the collision

        if (other.CompareTag("Lantern") || other.CompareTag("Ground"))
        {
            Destroy(gameObject); // Destroy bullet if it hits a lantern or ground
        }
    }

    // Non-trigger collision detection (when not using a Trigger Collider)
    protected void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Bullet collided with: " + other.collider.tag); // Log the collision

        if (other.collider.CompareTag("Ground"))
        {
            Destroy(gameObject); // Destroy bullet if it hits the ground
        }
    }

    // Called when the object is no longer visible by any camera
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy bullet when it goes out of camera view
    }
}
