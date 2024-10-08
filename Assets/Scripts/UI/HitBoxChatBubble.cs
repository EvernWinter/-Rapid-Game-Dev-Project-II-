using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxChatBubble : MonoBehaviour
{
    public bool isCollideWithObject = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollideWithObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
