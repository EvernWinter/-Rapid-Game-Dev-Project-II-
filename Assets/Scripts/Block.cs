using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType
    {
        Ordinary = 0,
        Copper = 1,
        Silver = 2,
        Gold = 3,
        LapisLazuli = 4,
        Diamond = 5
    };

    public enum ControlMode
    {
        DelayedReturn, // Block returns to normal physics after a delay
        ManualControl  // Block is never affected by physics unless controlled by player
    }

    public BlockType blockType;
    public ControlMode controlMode = ControlMode.DelayedReturn; // Default mode
    public float moveSpeed = 10f; // Speed at which the block moves towards the mouse position
    public float rotationIntensity = 5f; // How much the block rotates upon hitting an obstacle
    public float returnDelay = 2f; // Delay before returning to normal physics in DelayedReturn mode
    public float followSpeed = 5f; // Adjustable speed to control how fast the block follows the mouse
    public int blockIndex = 0;
    

    private Camera cam;
    private Rigidbody2D rb;
    private bool isPickedUp = false;
    [SerializeField] public bool isPlayerStand = false;
    [SerializeField] public bool isFrozen = false;
    


    void Start()
    {
        blockIndex = (int)blockType;
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    protected  virtual void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isPlayerStand && !isFrozen) // Right-click to pick up
        {
            // Cast a ray from the mouse position to check if the block is clicked
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // If the ray hits the block, pick it up
            if (hit.collider != null && hit.transform == transform && rb != null)
            {
                isPickedUp = true;
                isFrozen = false;

                // Set the block to dynamic mode and disable gravity
                rb.isKinematic = false;  // Ensure the block is dynamic when picked up
                rb.gravityScale = 0;     // Disable gravity while picked up
                rb.velocity = Vector2.zero; // Stop any movement
                StopAllCoroutines(); // Stop any return delay if it's active
            }
        }
        

        if (Input.GetMouseButtonUp(1)) // Release right-click
        {
            isPickedUp = false;

            if (controlMode == ControlMode.DelayedReturn)
            {
                // Start a coroutine to return to normal physics after a delay
                StartCoroutine(ReturnToNormalAfterDelay());
            }
            else if (controlMode == ControlMode.ManualControl)
            {
                // In ManualControl mode, freeze the block indefinitely
                FreezeBlock();
            }
        }
    }

    void FixedUpdate()
    {
        if (isPickedUp && rb != null)
        {
            // Get the mouse position in world space
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // Smoothly move the block towards the mouse position using Lerp for adjustable speed
            Vector2 newPosition = Vector2.Lerp(rb.position, mousePos, followSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition); // Directly move the rigidbody to the new position

            // Apply a random rotation for the levitation effect
            float randomRotation = Random.Range(-rotationIntensity, rotationIntensity);
            rb.angularVelocity = randomRotation;
        }
        else if (isFrozen && rb != null)
        {
            // Keep the block frozen, no movement or rotation
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

        UnFreezeBlock();
    }

    // When the block collides with something, apply some rotation if it's picked up
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPickedUp && rb != null)
        {
            // Apply random rotation upon collision to give a more natural levitating feel
            float randomRotation = Random.Range(-rotationIntensity, rotationIntensity);
            rb.AddTorque(randomRotation);
        }
        
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            ClosingToPlayer(true);
        }
    }

    public void ClosingToPlayer(bool isTrue = false)
    {
        if (isTrue)
        {
            FreezeBlock(); // Freeze the block if colliding with the player
            isPickedUp = false; // Set picked up to false when the block is frozen
        }
        else
        {
            isPickedUp = true;
            isFrozen = false;
        }
    }
    

    // Freeze the block's movement but still allow collisions
    public void FreezeBlock()
    {
        isFrozen = true;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.gravityScale = 0; // Disable gravity
            rb.isKinematic = true; // Set to kinematic, but still allow collisions
        }
    }
    
    public void UnFreezeBlock()
    {
        
        if (isFrozen && !isPlayerStand)
        {
            StartCoroutine(ReturnToNormalAfterDelay());
        }
        else if (!isFrozen && isPlayerStand)
        {
            FreezeBlock();
        }
        
        
    }

    // Return the block to normal physics after a delay
    IEnumerator ReturnToNormalAfterDelay()
    {
        FreezeBlock(); // Freeze the block while waiting for the delay
        yield return new WaitForSeconds(returnDelay); // Wait for the specified delay

        // Unfreeze the block and restore normal physics
        if (rb != null)
        {
            isFrozen = false;
            rb.gravityScale = 1; // Re-enable gravity
            rb.isKinematic = false; // Allow the block to be affected by physics again
        }
    }
}
