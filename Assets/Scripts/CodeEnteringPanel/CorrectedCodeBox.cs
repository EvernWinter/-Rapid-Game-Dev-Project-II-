using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CorrectedCodeBox : MonoBehaviour
{
    [SerializeField] private int _correctNumber;
    [SerializeField] private TMP_Text _text;
    
    // Start is called before the first frame update
    void Start()
    {
        HideNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCorrectNumber()
    {
        return _correctNumber;
    }
    
    public void AssignNumber(int newNumber)
    {
        _correctNumber = newNumber;
    }

    public void ShowNumber()
    {
        _text.text = _correctNumber.ToString();
    }

    public void HideNumber()
    {
        _text.text = "?";
    }
}
