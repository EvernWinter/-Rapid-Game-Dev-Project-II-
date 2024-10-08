using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [Header("Aim")] 
    [SerializeField] private Vector3 aimDirection;
    [SerializeField] private Transform staffPosition;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Transform playerTransform;
    public float minAimDistance = 1f; // Minimum allowed distance for aiming
    public float maxAimDistance = 5f; // Maximum allowed distance for aiming
    
    [Header("Bullet Animation")]
    [SerializeField] private Sprite[] startSprites;  // Array of sprites for start animation
    [SerializeField] private float animationSpeed = 0.1f; // Speed of the sprite animation
    [SerializeField] private SpriteRenderer wandSpriteRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Header("Pivot Point")]
    [SerializeField] private Transform hipPivot;

    [Header("Bullet Property")] 
    [SerializeField] private GameObject bullet;
    private float shootCooldown = 1f; // Cooldown
    private float nextShootTime = 0f; // Time at which the player can shoot again
    [SerializeField] private float bulletSpd;
    

    [Header("Player")] [SerializeField] private float xScale;

    
    [SerializeField] private TMP_Text cooldownText; // Reference to the TextMeshPro component
    private bool isCollectedGems = true; // Ensure this is set appropriately
    // Start is called before the first frame update
    void Start()
    {
        xScale = playerTransform.localScale.x;
        spriteRenderer = wandSpriteRenderer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        FaceTargetDirection();
        UpdateAimTransform();
        RotateStaffAroundHip();

        // Check for shooting input and manage cooldown
        if (Input.GetButtonDown("Fire1") && Time.time >= nextShootTime)
        {
            
            SoundManager.Instance.UseStaffFireSound();
            Shoot(bullet);
            nextShootTime = Time.time + shootCooldown;
        }

        if (Time.time >= nextShootTime)
        {
            GameManager.instance.SetIcon(1, "shoot");
        }
        else
        {
            GameManager.instance.SetIcon(0, "shoot");
        }

        // Update cooldown UI
        UpdateCooldownUI();

        Debug.DrawLine(shootPosition.position, mousePosition, Color.red);
    }

    private void RotateStaffAroundHip()
    {
        // Get the mouse position in world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure mouse position is in 2D plane

        // Calculate direction from the hip to the mouse
        aimDirection = (mousePosition - hipPivot.position).normalized;

        // Calculate the angle in degrees to rotate the wand
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Apply rotation around the player's hip (pivot point)
        staffPosition.RotateAround(hipPivot.position, Vector3.forward, angle - staffPosition.eulerAngles.z);
    }
    
    private void Shoot(GameObject bulletPrefab)
    {
        if (bulletPrefab == null) 
        {
            Debug.LogWarning("Bullet prefab is missing or destroyed!");
            return;
        }

        // Start the bullet animation
        StartCoroutine(AnimateStart());

        // Start a coroutine to handle bullet instantiation and movement delay
        StartCoroutine(InstantiateAndLaunchBullet(bulletPrefab));
    }

    private IEnumerator InstantiateAndLaunchBullet(GameObject bulletPrefab)
    {
        // Delay for 1 second before launching the bullet
        yield return new WaitForSeconds(0.3f);

        // Calculate the shoot direction using the mouse position
        Vector3 shootDirection = (mousePosition - shootPosition.position).normalized;

        // Calculate the angle to rotate the bullet
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        // Instantiate the bullet at the shoot position with the calculated rotation
        GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Set the bullet's velocity in the direction of shooting
            rb.velocity = shootDirection * bulletSpd;
        }
        else
        {
            Debug.LogWarning("Bullet prefab does not have a Rigidbody2D component!");
        }
    }

    private void UpdateAimTransform() // Aim with Mouse
    {
        // Get the mouse position in world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure mouse position is in 2D plane

        // Calculate the direction from the shoot position to the mouse
        aimDirection = (mousePosition - shootPosition.position).normalized;

        // Calculate the distance and clamp it
        float distance = Vector3.Distance(playerTransform.position, mousePosition);
        distance = Mathf.Clamp(distance, minAimDistance, maxAimDistance);

        // Update the aim direction
        aimDirection = shootPosition.position + aimDirection * distance;
    }

    private void FaceTargetDirection()
    {
        if (mousePosition.x > 0)
        {
            //playerTransform.localScale = new Vector3(xScale , playerTransform.localScale.y, playerTransform.localScale.z);
            staffPosition.localScale = new Vector3(staffPosition.localScale.x, Mathf.Abs(staffPosition.localScale.y), staffPosition.localScale.z);
        }
        else
        {
            //playerTransform.localScale = new Vector3(-xScale , playerTransform.localScale.y, playerTransform.localScale.z);
            staffPosition.localScale = new Vector3(staffPosition.localScale.x, -Mathf.Abs(staffPosition.localScale.y), staffPosition.localScale.z);
        }
    }
    
    private IEnumerator AnimateStart()
    {
        int index = 0;

        // Loop through the start sprites
        while (index < startSprites.Length)
        {
            spriteRenderer.sprite = startSprites[index]; // Set current sprite
            index++;
            yield return new WaitForSeconds(animationSpeed); // Wait before changing to the next sprite
        }
        spriteRenderer.sprite = null;
    }
    
    private void UpdateCooldownUI()
    {
        // Calculate remaining cooldown time
        float remainingTime = nextShootTime - Time.time;

        // If still on cooldown, update the text
        if (remainingTime > 0)
        {
            cooldownText.text = $"{remainingTime:F1}"; // Format to one decimal place
        }
        else
        {
            cooldownText.text = ""; // Display "Ready" when the cooldown is over
        }
    }
}
