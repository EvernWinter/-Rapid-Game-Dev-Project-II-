using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")] // Movement value
    [SerializeField] private float moveSpd;
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementInput;
    [SerializeField] private Rigidbody2D playerRb;
    public float PlayerMoveInput => playerRb.velocity.x;
    public bool IsOnGround => isOnGround;
    public bool IsJumping => isJumping;
    
    [Header("GroundCheck")] // GroundCheck value
    [SerializeField] private Transform feet;
    [SerializeField] private bool isOnGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius;
    
    
    [Header("Animation")] // Player animation value
    [SerializeField] private AnimationReferenceAsset idle;

    [Header("Variable")]
    [SerializeField] private bool isCutSceneOn = false;
    public bool IsCutSceneOn { get { return isCutSceneOn; } set { isCutSceneOn = value; } }

    public static PlayerController Instance;

    public Action OnJump;

    private bool isJumping = false; // To prevent stacking coroutines

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody2D>(); // Get the player rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCutSceneOn)
        {
            Move();
            Jump();
        }
    }
    
    

    private void Move()
    {
        movementInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(movementInput * moveSpd, playerRb.velocity.y);
    }

    private void Jump()
    {
        isOnGround = Physics2D.OverlapCircle(feet.position, checkRadius, groundLayer);
    
        // Check for overlap with Block
        Block overlappingBlock = GetOverlappingBlock();
        if (overlappingBlock != null)
        {
            overlappingBlock.FreezeBlock(); // Freeze the block if overlapping
        }

        if (isOnGround && Input.GetButtonDown("Jump") && !isJumping)
        {
            StartCoroutine(TriggerJump());
        }
    }

    private IEnumerator TriggerJump()
    {
        isJumping = true; // Prevent stacking the coroutine
        yield return new WaitForSeconds(0.01f);
        playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        OnJump?.Invoke();
        isJumping = false; // Reset the flag when the coroutine ends
    }

    private Block GetOverlappingBlock()
    {
        // Get all colliders within the check radius at the player's feet
        Collider2D[] colliders = Physics2D.OverlapCircleAll(feet.position, checkRadius);
        Block overlappingBlock = null;

        foreach (Block block in FindObjectsOfType<Block>())
        {
            block.isPlayerStand = false;
            //block.UnFreezeBlock(); // Reset to false for all blocks
        }

        foreach (Collider2D collider in colliders)
        {
            Block block = collider.GetComponent<Block>();
            if (block != null)
            {
                block.isPlayerStand = true; // Set true only for the overlapping block
                overlappingBlock = block; // Store the reference to the block
            }
        }

        return overlappingBlock; // Return the overlapping block if any
    }


    

    private void OnDrawGizmos() // Draw Gizmos on feet to check the colliding
    {
        if (feet != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(feet.position, checkRadius);
        }
    }

}
