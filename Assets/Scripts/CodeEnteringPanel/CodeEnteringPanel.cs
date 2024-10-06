using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CodeEnteringPanel : MonoBehaviour
{

    public static CodeEnteringPanel Instance;
    
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _checker;
    
    [SerializeField] private List<CorrectedCodeBox> _correctedCodeBoxes;
    [SerializeField] private List<InputCodeBox> _inputCodeBoxes;

    [SerializeField] private int _currentlyTyping;
    
    private bool _isAllCorrect;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        _currentlyTyping = 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        AssignNumbersToCorrectedBoxes();    
    }

    // Update is called once per frame
    void Update()
    {
        if (!_panel.activeSelf)
            return;

        // Check for number keys input (1-9) to update the currently typing box
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                _inputCodeBoxes[_currentlyTyping].AssignNumber(i);
            }
        }

        // Example to navigate through input boxes
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentlyTyping = Mathf.Max(0, _currentlyTyping - 1);  // Move to the previous box
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentlyTyping = Mathf.Min(_inputCodeBoxes.Count - 1, _currentlyTyping + 1);  // Move to the next box
        }
        UpdateCurrentInputBoxHighlight();
    }
    
    // Method to update the highlight
    private void UpdateCurrentInputBoxHighlight()
    {
        // Unhighlight all boxes
        foreach (var box in _inputCodeBoxes)
        {
            box.Unhighlight();
        }

        // Highlight the currently selected box
        _inputCodeBoxes[_currentlyTyping].Highlight();
    }


    public void Show()
    {
        _panel.SetActive(true);
    }

    public void Hide()
    {
        _panel.SetActive(false);
    }

    public void CheckIfInputIsCorrected()
    {
        _isAllCorrect = true;  // Assume all are correct initially

        for (int i = 0; i < _correctedCodeBoxes.Count; i++)
        {
            if (_correctedCodeBoxes[i].GetCorrectNumber() == _inputCodeBoxes[i].GetCurrentNumber())
            {
                _correctedCodeBoxes[i].ShowNumber();
            }
            else
            {
                _correctedCodeBoxes[i].HideNumber();
                
                _isAllCorrect = false;  // Set to false if any box is incorrect
            }
        }

        if (_isAllCorrect)
        {
            _checker.color = Color.green;
        }
        else
        {
            _checker.color = Color.red;
        }
        
        // You can now use _isAllCorrect to determine if all input numbers were correct
        Debug.Log("Are all numbers correct? " + _isAllCorrect);
    }
    
    public void AssignNumbersToCorrectedBoxes()
    {
        for (int i = 0; i < _correctedCodeBoxes.Count; i++)
        {
            _correctedCodeBoxes[i].AssignNumber(ClockRandom.Instance.RandomNumbers[i]);
        }
    }
}
