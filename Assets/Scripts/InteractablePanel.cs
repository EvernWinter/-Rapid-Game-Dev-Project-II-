using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePanel : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Make sure this method is public
    public void Show()
    {
        gameObject.SetActive(true);
        
    }

    // Make sure this method is public
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
