using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputCodeBox : MonoBehaviour
{
    [SerializeField] private int _currentNumber;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _boxImage; // Reference to the background image of the box
    
    private Color normalColor = Color.white;  // Default color
    private Color highlightColor = Color.yellow;  // Color when the box is highlighted
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the box to the normal color
        Unhighlight();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _currentNumber.ToString();
    }

    // Method to highlight the box
    public void Highlight()
    {
        _boxImage.color = highlightColor;
    }

    // Method to unhighlight the box
    public void Unhighlight()
    {
        _boxImage.color = normalColor;
    }
    
    public int GetCurrentNumber()
    {
        return _currentNumber;
    }

    public void AssignNumber(int newNumber)
    {
        _currentNumber = newNumber;
    }
    
    [ContextMenu("Increase Number")]
    public void IncreaseNumber()
    {
        _currentNumber = Math.Clamp(_currentNumber + 1, 0, 9);
    }

    [ContextMenu("Decrease Number")]
    public void DecreaseNumber()
    {
        _currentNumber = Math.Clamp(_currentNumber - 1, 0, 9);
    }
    
}
