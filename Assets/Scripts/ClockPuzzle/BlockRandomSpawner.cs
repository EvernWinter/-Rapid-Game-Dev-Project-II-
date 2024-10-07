using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRandomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _codeBlocks;    // List of existing blocks to move
    [SerializeField] private List<Transform> _spawnPoints;    // List of spawn points
    private List<Transform> _availableSpawnPoints;            // Track available spawn points

    private void Start()
    {
        // Get all child objects of this GameObject and store them as spawn points
        _spawnPoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            _spawnPoints.Add(child);
        }

        // Initialize available spawn points
        _availableSpawnPoints = new List<Transform>(_spawnPoints);

        // Move all code blocks to random spawn points
        MoveBlocksToRandomPoints();
    }

    private void Update()
    {
        // You can trigger MoveBlocksToRandomPoints() from Update if needed, based on conditions
    }

    /// <summary>
    /// Moves each block to a random available spawn point and ensures no repetition.
    /// </summary>
    public void MoveBlocksToRandomPoints()
    {
        if (_codeBlocks.Count == 0 || _spawnPoints.Count == 0)
        {
            Debug.LogWarning("No blocks or spawn points available!");
            return;
        }

        // Shuffle blocks and spawn points to ensure randomness
        ShuffleList(_codeBlocks);
        ShuffleList(_availableSpawnPoints);

        // Iterate through each block and move it to a random spawn point
        for (int i = 0; i < _codeBlocks.Count; i++)
        {
            if (_availableSpawnPoints.Count == 0) 
            {
                Debug.LogWarning("No more available spawn points!");
                break;
            }

            // Pick the first available spawn point
            Transform spawnPoint = _availableSpawnPoints[0];

            // Move the block to the spawn point
            _codeBlocks[i].transform.position = spawnPoint.position;

            // Remove the used spawn point from the list
            _availableSpawnPoints.RemoveAt(0);
        }

        // Reset the available spawn points if needed for future usage
        ResetAvailableSpawnPoints();
    }

    /// <summary>
    /// Resets the available spawn points back to the original spawn points.
    /// </summary>
    private void ResetAvailableSpawnPoints()
    {
        _availableSpawnPoints = new List<Transform>(_spawnPoints);
    }

    /// <summary>
    /// Shuffle a list in place to ensure randomization.
    /// </summary>
    /// <typeparam name="T">The type of the list.</typeparam>
    /// <param name="list">The list to shuffle.</param>
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
