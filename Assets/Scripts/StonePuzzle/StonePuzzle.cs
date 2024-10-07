using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePuzzle : MonoBehaviour
{
    [SerializeField] private GameObject stonePrefab; // Reference to the stone prefab
    [SerializeField] private Transform[] stonePositions; // Positions to instantiate stones
    [SerializeField] private Sprite[] stoneSprites; // Array of sprites for the stones
    [SerializeField] private Transform[] snapPoints; // Snap points for the stones
    [SerializeField] private float snapDistance = 0.5f; // Distance within which stones can snap
    public bool isStonePuzzlePass = false; // Indicates if the puzzle is complete
    [SerializeField] private GameObject blueGems;

    private List<int> assignedNumbers; // Numbers assigned to stones
    private int[] correctAnswerSequence; // The correct answer sequence for snap points
    private bool isPuzzlePassedOnce = false; // Track if the puzzle has been completed

    private void Start()
    {
        assignedNumbers = new List<int>();
        correctAnswerSequence = new int[snapPoints.Length];

        AssignNumbersToStones();
        GenerateCorrectAnswerSequence();
    }

    private void Update()
    {
        CheckSnapStones();

        // If all snap points are occupied and the puzzle has not been completed yet
        if (CheckIfAllSnapPointsOccupied() && !isPuzzlePassedOnce)
        {
            StartCoroutine(CheckPuzzleCompletionWithDelay());
        }
    }

    private void AssignNumbersToStones()
    {
        // Create a list of numbers 1 to 5 and shuffle them
        List<int> availableNumbers = new List<int> { 1, 2, 3, 4, 5 };
        for (int i = 0; i < stonePositions.Length; i++)
        {
            int randomIndex = Random.Range(0, availableNumbers.Count);
            int number = availableNumbers[randomIndex];
            availableNumbers.RemoveAt(randomIndex); // Remove the assigned number to ensure uniqueness

            assignedNumbers.Add(number); // Assign the unique number

            // Instantiate a stone at the specified position
            GameObject stoneObj = Instantiate(stonePrefab, stonePositions[i].position, Quaternion.identity);
            Stone stone = stoneObj.GetComponent<Stone>(); // Get the Stone component
            stone.stoneNumber = number; // Assign the number to the stone

            // Assign the sprite directly (ensuring uniqueness)
            stone.SetSprite(stoneSprites[i % stoneSprites.Length]); // Adjust as needed to ensure unique sprites
        }
    }

    private void GenerateCorrectAnswerSequence()
    {
        // Shuffle the correct answer sequence for the snap points
        List<int> numbersForSnapPoints = new List<int> { 1, 2, 3, 4, 5 };
        for (int i = 0; i < snapPoints.Length; i++)
        {
            int randomIndex = Random.Range(0, numbersForSnapPoints.Count);
            correctAnswerSequence[i] = numbersForSnapPoints[randomIndex];
            numbersForSnapPoints.RemoveAt(randomIndex); // Ensure no repeats
        }

        // Debugging: Print the correct answer sequence
        Debug.Log("Correct Answer Sequence: " + string.Join(", ", correctAnswerSequence));
    }

    private void CheckSnapStones()
    {
        // Find all stones in the scene
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Stone");

        foreach (GameObject stone in stones)
        {
            Stone stoneComponent = stone.GetComponent<Stone>();
            bool snappedToAnyPoint = false; // Track if this stone has successfully snapped to any point

            // Check the distance between the stone and each snap point
            for (int i = 0; i < snapPoints.Length; i++)
            {
                float distance = Vector2.Distance(stone.transform.position, snapPoints[i].position);

                // If the stone is close enough to snap
                if (distance < snapDistance)
                {
                    // Snap the stone to the snap point
                    stone.transform.position = snapPoints[i].position; // Snap directly to the position
                    stone.transform.rotation = Quaternion.identity; // Reset rotation if needed
                    stone.transform.SetParent(snapPoints[i]); // Set the stone as a child of the snap point

                    snappedToAnyPoint = true; // Mark that this stone has successfully snapped
                    break; // Break the loop after snapping
                }
            }

            // If the stone didn't snap, allow it to be pulled out if it's far enough from all snap points
            if (!snappedToAnyPoint)
            {
                for (int i = 0; i < snapPoints.Length; i++)
                {
                    float distance = Vector2.Distance(stone.transform.position, snapPoints[i].position);

                    // Allow the stone to be pulled out by resetting its position if it was previously snapped
                    if (distance >= snapDistance + 0.2f) // Added offset for better pulling
                    {
                        // If the stone is currently a child of a snap point, remove it
                        if (stone.transform.parent == snapPoints[i])
                        {
                            stone.transform.SetParent(null); // Unparent the stone
                        }
                    }
                }
            }
        }
    }

    private bool CheckIfAllSnapPointsOccupied()
    {
        foreach (Transform snapPoint in snapPoints)
        {
            if (snapPoint.childCount == 0)
            {
                return false; // If any snap point is empty, return false
            }
        }
        return true; // All snap points are occupied
    }

    private IEnumerator CheckPuzzleCompletionWithDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before checking

        // Check if stones are in the correct order
        for (int i = 0; i < snapPoints.Length; i++)
        {
            Stone stoneComponent = GetStoneAtSnapPoint(snapPoints[i].position);
            if (stoneComponent != null && stoneComponent.stoneNumber == correctAnswerSequence[i])
            {
                Debug.Log($"Stone {stoneComponent.stoneNumber} is in the correct position.");
            }
            else
            {
                // Debugging information
                //Debug.Log($"Puzzle is not solved correctly. Expected: {correctAnswerSequence[i]}, Found: {stoneComponent?.stoneNumber}");
                yield break; // Exit the coroutine if the current stone does not match
            }
        }

        // If all stones are in the correct positions
        isPuzzlePassedOnce = true; // Mark puzzle as completed
        isStonePuzzlePass = true;
        blueGems.GetComponent<Gems>().PuzzlePassed();
        Debug.Log("Puzzle Completed!");
    }

    private Stone GetStoneAtSnapPoint(Vector3 snapPointPosition)
    {
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Stone");
        foreach (var stone in stones)
        {
            if (Vector2.Distance(stone.transform.position, snapPointPosition) < snapDistance)
            {
                return stone.GetComponent<Stone>();
            }
        }
        return null;
    }
}
