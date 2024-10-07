using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gems : MonoBehaviour
{
    private Collider2D gemCollider;
    private SpriteRenderer gemRenderer;
    public GameObject cage;
    private SpriteRenderer cageRenderer;
    [SerializeField] private bool puzzlePassed = false;
    [SerializeField] private bool drop = false;
    [SerializeField] private Transform dropPosition; // Final position to drop to
    

    public float dropSpeed = 1.0f;
    public float fadeSpeed = 1.0f;

    void Start()
    {
        gemCollider = GetComponent<Collider2D>();
        gemRenderer = GetComponent<SpriteRenderer>();
        cageRenderer = cage.GetComponent<SpriteRenderer>(); // Ensure cage is assigned and exists
    }

    void Update()
    {
        if (puzzlePassed)
        {
            if (drop)
            {
                DropGem();
            }

            FadeCage();
        }
    }

    public void PuzzlePassed()
    {
        puzzlePassed = true;
        gemCollider.isTrigger = true; // Set gem collider to trigger
    }

    private void DropGem()
    {
        // Move the gem towards the drop position
        if (dropPosition != null)
        {
            // Calculate the step size
            float step = dropSpeed * Time.deltaTime;

            // Move the gem towards the drop position
            transform.position = Vector3.MoveTowards(transform.position, dropPosition.position, step);

            // Check if the gem has reached the drop position
            if (Vector3.Distance(transform.position, dropPosition.position) < 0.01f)
            {
                // Snap to the exact drop position to avoid floating point inaccuracies
                transform.position = dropPosition.position;

                drop = false; // Stop dropping once it reaches the position
                // Optionally, you can set canDrop to false if you want to prevent further dropping
                // canDrop = false; 
            }
        }
    }

    private void FadeCage()
    {
        // Check if the cageRenderer is null before proceeding
        if (cageRenderer != null)
        {
            // Slowly fade out the cage by reducing its alpha value
            Color cageColor = cageRenderer.color;
            cageColor.a -= fadeSpeed * Time.deltaTime;
            cageRenderer.color = cageColor;

            if (cageColor.a <= 0f)
            {
                Destroy(cage); // Destroy the cage once it's fully transparent
                cageRenderer = null; // Set cageRenderer to null to prevent further access
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle gem interaction with player, e.g., making it disappear
            gemRenderer.enabled = false; // Make the gem disappear visually
            Destroy(gameObject, 1f); // Optionally destroy the gem after 1 second
        }
        else if (other.CompareTag("Ground")) // Check for ground collision
        {
            drop = false; // Stop the gem from dropping
        }
    }
    
    
}
