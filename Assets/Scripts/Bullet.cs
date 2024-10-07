using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Animation")]
    [SerializeField] private Sprite[] startSprites;  // Array of sprites for start animation
    [SerializeField] private Sprite[] flyingSprites; // Array of sprites for flying animation
    [SerializeField] private Sprite[] hitSprites; // Array of sprites for hit animation
    [SerializeField] private float animationSpeed = 0.1f; // Speed of the sprite animation
    [SerializeField] private float bulletSpeed = 5f; // Speed of the bullet after start animation

    private SpriteRenderer spriteRenderer;
    private bool isHit = false; // To track if the bullet has hit something
    private Rigidbody2D rb; // Reference to Rigidbody2D

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AnimateFlying());
    }
    

    private IEnumerator AnimateFlying()
    {
        int index = 0;

        // Loop through the flying sprites
        while (!isHit)
        {
            spriteRenderer.sprite = flyingSprites[index]; // Set current sprite
            index = (index + 1) % flyingSprites.Length; // Loop through the array
            yield return new WaitForSeconds(animationSpeed); // Wait before changing to the next sprite
        }
        
    }

    private IEnumerator AnimateHit()
    {
        int index = 0;

        // Stop the bullet after the hit animation
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop the bullet's movement
            rb.isKinematic = true; // Set the bullet to kinematic to stop any physics interaction
        }
        // Loop through the hit sprites
        while (index < hitSprites.Length)
        {
            spriteRenderer.sprite = hitSprites[index]; // Set current sprite
            index++;
            yield return new WaitForSeconds(animationSpeed); // Wait before changing to the next sprite
        }

        Destroy();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lantern") || other.CompareTag("Ground") || other.CompareTag("Object") || other.CompareTag("Stone"))
        {
            //SoundManager.Instance.FireImpactSound();
            isHit = true; // Set the hit flag to true
            StartCoroutine(AnimateHit());
        }
        
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isHit = true; // Set the hit flag to true
            StopAllCoroutines(); // Stop animations
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(); // Destroy bullet when it goes out of camera view
    }

    private void Destroy()
    {
        StopAllCoroutines(); // Stop animations
        Destroy(gameObject);
    }
}
