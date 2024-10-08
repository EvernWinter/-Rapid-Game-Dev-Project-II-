using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoneHint : MonoBehaviour
{
    [SerializeField] private TMP_Text[] stoneTexts; // Array to hold references to TMP texts

    void Start()
    {
        GatherStonesAndAssignNumbers();
    }

    private void GatherStonesAndAssignNumbers()
    {
        // Find all objects with the "Stone" tag
        GameObject[] stoneObjects = GameObject.FindGameObjectsWithTag("Stone");

        // Loop through each found stone and get its Stone component
        for (int i = 0; i < stoneObjects.Length; i++)
        {
            Stone stoneComponent = stoneObjects[i].GetComponent<Stone>(); // Get the Stone component

            if (stoneComponent != null)
            {
                int stoneNumber = stoneComponent.stoneNumber; // Get the stone number

                // Ensure there is a corresponding TMP text for each stone
                if (i < stoneTexts.Length)
                {
                    // Set the text of the corresponding TMP to the stone's number
                    stoneTexts[i].text = stoneNumber.ToString();
                }
            }
        }
    }
}
