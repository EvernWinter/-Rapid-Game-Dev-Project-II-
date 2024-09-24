using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockPuzzle : MonoBehaviour
{
    [SerializeField] GameObject[] specialBlockHolders = new GameObject[3];
    [SerializeField] bool[] isHolderOccupied = new bool[3];
    [SerializeField] int[] blockTypeIndex = new int[3];
    [SerializeField] float minBlockSnapDistance = 1f;

    private void Start()
    {
        for(int i = 0; i < blockTypeIndex.Length; i++)
        {
            
        }
    }

    private void Update()
    {
        GameObject[] specialBlocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject specialBlock in specialBlocks)
        {
            if (isHolderOccupied[0] == false)
            {
                if (Vector2.Distance(specialBlockHolders[0].transform.position, specialBlock.transform.position) < minBlockSnapDistance)
                {
                    specialBlock.transform.position = specialBlockHolders[0].transform.position;
                    //isHolderOccupied[0] = true;
                }

            }
            
            if (isHolderOccupied[1] == false)
            {
                if (Vector2.Distance(specialBlockHolders[1].transform.position, specialBlock.transform.position) < minBlockSnapDistance)
                {
                    specialBlock.transform.position = specialBlockHolders[1].transform.position;
                    //isHolderOccupied[1] = true;
                }
            }
            
            if (isHolderOccupied[2] == false)
            {
                if (Vector2.Distance(specialBlockHolders[2].transform.position, specialBlock.transform.position) < minBlockSnapDistance)
                {
                    specialBlock.transform.position = specialBlockHolders[2].transform.position;
                    //isHolderOccupied[2] = true;
                }
            }
        }
    }
}
