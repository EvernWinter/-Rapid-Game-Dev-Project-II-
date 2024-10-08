using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    // Make sure this method is public
    public void Show()
    {
        _panel.SetActive(true);
    }

    // Make sure this method is public
    public void Hide()
    {
        _panel.SetActive(false);
    }
}
