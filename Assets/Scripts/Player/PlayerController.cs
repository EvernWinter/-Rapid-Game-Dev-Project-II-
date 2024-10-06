using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")] //Movement value
    [SerializeField] private float moveSpd;
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementInput;
    [SerializeField] private Rigidbody2D playerRb;
    
    [Header("GroundCheck")] //GroundCheck value
    [SerializeField] private Transform feet;
    [SerializeField] private bool isOnGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius;
    
    [Header("Animation")] //Player animation value
    [SerializeField] private AnimationReferenceAsset idle;

    [Header("Variable")]
    [SerializeField] private bool isCutSceneOn = false;
    public bool IsCutSceneOn { get { return isCutSceneOn; } set { isCutSceneOn = value; } }

    public static PlayerController Instance;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody2D>(); //Get the player rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCutSceneOn)
        {
            Move();
            Jump();
        }
        
    }

    private void Move()
    {
        movementInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(movementInput * moveSpd,playerRb.velocity.y);
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

        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }
    }

    private Block GetOverlappingBlock()
    {
        // Use a circle overlap check for the feet
        Collider2D[] colliders = Physics2D.OverlapCircleAll(feet.position, checkRadius);
    
        foreach (Collider2D collider in colliders)
        {
            Block block = collider.GetComponent<Block>();
            if (block != null)
            {
                return block; // Return the first overlapping block found
            }
        }
        return null; // No overlapping block found
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
