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
    
    [SerializeField] private List<CorrectedCodeBox> correctedCodeBoxes;
    [SerializeField] private List<InputCodeBox> inputCodeBoxes;

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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        AssignNumbersToCorrectedBoxes();    
    }

    // Update is called once per frame
    void Update()
    {

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

        for (int i = 0; i < correctedCodeBoxes.Count; i++)
        {
            if (correctedCodeBoxes[i].GetCorrectNumber() == inputCodeBoxes[i].GetCurrentNumber())
            {
                correctedCodeBoxes[i].ShowNumber();
            }
            else
            {
                correctedCodeBoxes[i].HideNumber();
                
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
        for (int i = 0; i < correctedCodeBoxes.Count; i++)
        {
            correctedCodeBoxes[i].AssignNumber(ClockRandom.Instance.RandomNumbers[i]);
        }
    }
}
