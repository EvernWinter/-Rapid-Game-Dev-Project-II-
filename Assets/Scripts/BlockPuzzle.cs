using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Block;

public class BlockPuzzle : MonoBehaviour
{
    [SerializeField] GameObject[] specialBlockHolders = new GameObject[3];
    [SerializeField] bool[] isHolderOccupied = new bool[3];
    [SerializeField] int[] blockTypeIndex = new int[3];
    [SerializeField] float minBlockSnapDistance = 1f;
    [SerializeField] private Text[] text_BlockType = new Text[3];
    [SerializeField] int[] randomIndexes = new int[3];
    [SerializeField] private int numHolderOccupied = 0;

    public bool isBlockPuzzlePass = false;

    public static BlockPuzzle Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        randomIndexes = GenerateUniqueRandomNumbers(3, 1, 5);
    }

    private void Update()
    {
        GameObject[] specialBlocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject specialBlock in specialBlocks)
        {
            int blockIndex = specialBlock.GetComponent<Block>().blockIndex;

            for (int i = 0; i < specialBlockHolders.Length; i++)
            {
                if (/*isHolderOccupied[i] == false &&*/ blockIndex == randomIndexes[i])
                {
                    if (Vector2.Distance(specialBlockHolders[i].transform.position, specialBlock.transform.position) < minBlockSnapDistance)
                    {
                        specialBlock.transform.position = specialBlockHolders[i].transform.position;
                        specialBlock.transform.rotation = specialBlockHolders[i].transform.rotation;
                        Debug.Log($"Block {blockIndex} matched Holder {i}. Snapped successfully.");

                        if (isHolderOccupied[i] == false)
                        {
                            isHolderOccupied[i] = true;
                            numHolderOccupied++;
                        }
                    }
                }
            }
        }

        if(numHolderOccupied >= 3)
        {
            isBlockPuzzlePass = true;
        }
    }

    private string GetBlockTypeString(int index)
    {
        string text = "";

        switch(index)
        {
            case 1:
                text = "C";
                break;

            case 2:
                text = "S";
                break;

            case 3:
                text = "G";
                break;

            case 4:
                text = "L";
                break;

            case 5:
                text = "D";
                break;
        }
        return text;
    }

    private int[] GenerateUniqueRandomNumbers(int count, int min, int max)
    {
        int[] numbers = new int[count];
        List<int> availableNumbers = new List<int>();

        // Populate the list with numbers from min to max
        for (int i = min; i <= max; i++)
        {
            availableNumbers.Add(i);
        }

        // Randomly pick unique numbers
        for (int i = 0; i < count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, availableNumbers.Count);
            numbers[i] = availableNumbers[randomIndex];
            availableNumbers.RemoveAt(randomIndex);
        }

        for(int i = 0; i < numbers.Length; i++)
        {

            text_BlockType[i].text = $"{GetBlockTypeString(numbers[i])}";
        }

        return numbers;
    }
}
