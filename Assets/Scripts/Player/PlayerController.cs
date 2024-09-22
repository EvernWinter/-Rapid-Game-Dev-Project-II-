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
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody2D>(); //Get the player rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        movementInput = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(movementInput * moveSpd,playerRb.velocity.y);
    }

    private void Jump()
    {
        isOnGround = Physics2D.OverlapCircle(feet.position, checkRadius, groundLayer);
        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmos () //Draw Gizmos on feet to check the colliding
    {
        if (feet != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(feet.position, checkRadius);
        }
    }

}
