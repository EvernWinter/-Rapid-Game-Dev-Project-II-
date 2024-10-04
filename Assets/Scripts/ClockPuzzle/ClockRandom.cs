using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Random = UnityEngine.Random;

public enum ColorEnum
{
    Red,
    Green,
    Blue,
    Yellow,
    Purple
}

public class ClockRandom : MonoBehaviour
{
    public static ClockRandom Instance;
    
    private List<int> randomNumbers = new List<int>(); //Answer
    public List<int> RandomNumbers => randomNumbers; //Answer
    private LinkedList<string> numbersOnBox = new LinkedList<string>(); //Block
    public TMP_Text numberText;
    public TMP_Text[] boxText;
    private List<ColorEnum> availableColors = new List<ColorEnum>() { ColorEnum.Red, ColorEnum.Green, ColorEnum.Blue, ColorEnum.Yellow, ColorEnum.Purple };
    private Dictionary<int, ColorEnum> numberColorMapping = new Dictionary<int, ColorEnum>(); // To store number-color pairs

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
        
        RandomClockPuzzle();
        OutPutNumber();
        SetNumberInBox();
        
        // Shuffle box contents
        BoxShuffle(boxText);
    
        // Assign shuffled text to 3 random blocks
        AssignTextToRandomBlocks(boxText);
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomClockPuzzle()
    {
        // Generate random numbers and assign colors
        for (int i = 1; i <= 5; i++)
        {
            int randomNumber = Random.Range(1, 10);
            Debug.Log("Random Number: " + randomNumber); 
            if (!randomNumbers.Contains(randomNumber))
            {
                randomNumbers.Add(randomNumber);
                
                // Assign a unique color
                if (availableColors.Count > 0)
                {
                    ColorEnum randomColor = availableColors[Random.Range(0, availableColors.Count)];
                    numberColorMapping.Add(randomNumber, randomColor);
                    availableColors.Remove(randomColor); // Remove to avoid repeated colors
                }
            }
            else
            {
                i--; // Retry for a new number
                
            }
        }
    }

    public void OutPutNumber()
    {
        string listContents = "Random Numbers: ";
        foreach (int number in randomNumbers)
        {
            ColorEnum color = numberColorMapping[number];
            string colorHexCode = GetColorHexCode(color);
            
            listContents += $"<color={colorHexCode}>{number}</color> "; // Add number with color
        }
        
        numberText.text = listContents;
        
        Debug.Log(listContents); 
    }

    public void SetNumberInBox()
    {
        // Ensure randomNumbers has enough numbers
        if (randomNumbers.Count < 5)
        {
            Debug.LogError("Not enough random numbers generated.");
            return;
        }

        // Clear all boxes first
        foreach (var box in boxText)
        {
            box.text = string.Empty;
        }

        // Box 1: Contains the first two numbers
        int sizeBox1 = 2;
        for (int index = 0; index < sizeBox1; index++)
        {
            int numberToAdd = randomNumbers[index];
            ColorEnum color = numberColorMapping[numberToAdd];
            string colorHexCode = GetColorHexCode(color);
            boxText[0].text += $"<color={colorHexCode}>{numberToAdd}</color> ";
        }

        // Box 2: Contains the second number from Box 1 and the next number
        int startIndexBox2 = 1; // Starting from the second number in Box 1
        int sizeBox2 = 2;

        for (int index = startIndexBox2; index < startIndexBox2 + sizeBox2; index++)
        {
            if (index < randomNumbers.Count)
            {
                int numberToAdd = randomNumbers[index];
                ColorEnum color = numberColorMapping[numberToAdd];
                string colorHexCode = GetColorHexCode(color);
                boxText[1].text += $"<color={colorHexCode}>{numberToAdd}</color> ";
            }
        }

        // Box 3: Starts with the last number from Box 2 and includes the remaining numbers
        int startIndexBox3 = 2; // From the second number (last number from Box 2) 
        int sizeBox3 = randomNumbers.Count - startIndexBox3; // Include all remaining numbers

        for (int index = startIndexBox3; index < randomNumbers.Count; index++)
        {
            int numberToAdd = randomNumbers[index];
            ColorEnum color = numberColorMapping[numberToAdd];
            string colorHexCode = GetColorHexCode(color);
            boxText[2].text += $"<color={colorHexCode}>{numberToAdd}</color> ";
        }

        BoxShuffle(boxText);
        
        // Optional: Log the final contents of each box
        Debug.Log($"Box 1 contents: {boxText[0].text}");
        Debug.Log($"Box 2 contents: {boxText[1].text}");
        Debug.Log($"Box 3 contents: {boxText[2].text}");
    }

    private void BoxShuffle(TMP_Text[] box)
    {
        // Create a temporary list to hold the current box contents
        List<string> boxContents = new List<string>();

        // Fill the list with the current contents of each box
        foreach (var b in box)
        {
            boxContents.Add(b.text.Trim()); // Add trimmed text to remove leading/trailing spaces
        }

        // Shuffle the contents using Fisher-Yates algorithm
        for (int i = boxContents.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // Random index from 0 to i
            // Swap the elements
            string temp = boxContents[i];
            boxContents[i] = boxContents[j];
            boxContents[j] = temp;
        }

        // Assign the shuffled contents back to the boxes
        for (int i = 0; i < box.Length; i++)
        {
            box[i].text = boxContents[i];
        }
    }
    
    private void AssignTextToRandomBlocks(TMP_Text[] box)
    {
        // Step 1: Collect the box contents into a list
        List<string> boxContents = new List<string>();
        foreach (var b in box)
        {
            boxContents.Add(b.text.Trim()); // Add trimmed text to avoid leading/trailing spaces
        }

        // Step 2: Ensure there are at least 3 content entries
        if (boxContents.Count < 3)
        {
            Debug.LogError("Not enough text boxes to assign.");
            return;
        }

        // Step 3: Get all ClockBlock instances
        List<ClockBlock> clockBlocks = new List<ClockBlock>(FindObjectsOfType<ClockBlock>());

        if (clockBlocks.Count < 3)
        {
            Debug.LogError("Not enough ClockBlock instances to assign text.");
            return;
        }

        // Step 4: Shuffle the blocks to randomize them
        for (int i = clockBlocks.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); // Random index from 0 to i
            ClockBlock temp = clockBlocks[i];
            clockBlocks[i] = clockBlocks[j];
            clockBlocks[j] = temp;
        }

        // Step 5: Assign the first 3 shuffled box texts to the first 3 blocks
        for (int i = 0; i < 3; i++) // Only assign to 3 blocks
        {
            ClockBlock selectedBlock = clockBlocks[i];
            string assignedText = boxContents[i];

            // Set the number text in the block (assuming you have a method in ClockBlock to set this)
            selectedBlock.AssignText(assignedText);

            Debug.Log($"Assigned {assignedText} to block {i}");
        }
    }


    

    private string GetColorHexCode(ColorEnum colorEnum)
    {
        switch (colorEnum)
        {
            case ColorEnum.Red:
                return "#FF0000";
            case ColorEnum.Green:
                return "#00FF00";
            case ColorEnum.Blue:
                return "#0000FF";
            case ColorEnum.Yellow:
                return "#FFFF00";
            case ColorEnum.Purple:
                return "#FF00FF";
            default:
                return "#FFFFFF"; // Default white color
        }
    }
}
