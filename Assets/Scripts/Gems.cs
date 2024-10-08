using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gems : MonoBehaviour
{
    //SerializeField] public enum GemType { Red, Blue , Green};
    [SerializeField] public GemStoneTypeEnum gemType; 

    private Collider2D gemCollider;
    private SpriteRenderer gemRenderer;
    public GameObject cage;
    private SpriteRenderer cageRenderer;
    [SerializeField] public bool puzzlePassed = false;
    [SerializeField] private bool drop = false;
    [SerializeField] private Transform dropPosition; // Final position to drop to
    [SerializeField] public GameObject[] portal;
       

    public float dropSpeed = 1.0f;
    public float fadeSpeed = 1.0f;

    void Start()
    {
        gemCollider = GetComponent<Collider2D>();
        gemRenderer = GetComponent<SpriteRenderer>();
        if (cage != null)
        {
            cageRenderer = cage.GetComponent<SpriteRenderer>(); // Ensure cage is assigned and exists
        }
        gemCollider.enabled = false;
    }

    void Update()
    {
        if (puzzlePassed || Input.GetKeyDown(KeyCode.K))
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
        gemCollider.enabled = true;
        drop = true;
        puzzlePassed = true;
        if (gemCollider != null)
        {
            gemCollider.isTrigger = true; // Set gem collider to trigger
        }
        
    }

    private void DropGem()
    {
        // Move the gem towards the drop position
        if (dropPosition != null)
        {
            // Calculate the step size
            float step = dropSpeed * Time.deltaTime;

            // Move the gem towards the drop position
            //transform.position = Vector3.MoveTowards(transform.position, dropPosition.position, step)
            transform.position = Vector3.MoveTowards(transform.position, dropPosition.position, 0.07f);

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

    private void CollectGem()
    {
        switch(gemType)
        {
            case GemStoneTypeEnum.Blue:
                GameManager.instance.isCollectGemStone_Blue = true;
                break;

            case GemStoneTypeEnum.Red:
                GameManager.instance.isCollectGemStone_Red = true;
                
                break;

            case GemStoneTypeEnum.Green:
                GameManager.instance.isCollectGemStone_Green = true;
                break;
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
            for (int i = 0; i < portal.Length; i++)
            {
                portal[i].GetComponent<Portal>().open = true;
            }
            // Handle gem interaction with player, e.g., making it disappear
            gemRenderer.enabled = false; // Make the gem disappear visually
            CollectGem();
            Block.CollectGem(gemType);
            Destroy(gameObject, 1f); // Optionally destroy the gem after 1 second
            Debug.Log("Gem Collected");
        }
        else if (other.CompareTag("Ground")) // Check for ground collision
        {
            drop = false; // Stop the gem from dropping
        }
    }
    
    
}
