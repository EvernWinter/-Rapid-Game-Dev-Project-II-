using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputCodeBox : MonoBehaviour
{
    [SerializeField] private int _currentNumber;
    [SerializeField] private TMP_Text _text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _currentNumber.ToString();
    }

    public int GetCurrentNumber()
    {
        return _currentNumber;
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
