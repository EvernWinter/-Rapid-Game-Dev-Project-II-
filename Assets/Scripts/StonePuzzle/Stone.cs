using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] public int stoneNumber; // This will hold the assigned number (1-5)
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    // Method to set the sprite based on the stone number
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    // Method to send both number and SpriteRenderer
    public (int, SpriteRenderer) SendNumberAndSprite()
    {
        return (stoneNumber, spriteRenderer);
    }
}
