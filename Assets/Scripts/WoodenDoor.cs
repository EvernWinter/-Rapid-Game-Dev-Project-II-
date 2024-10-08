using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenDoor : MonoBehaviour
{
    public enum Mode
    {
        BlockTrigger,    // Block is required to open the door
        CrystalTrigger   // Crystal is required to open the door
    }

    // Enum to track the current state of the door
    public enum DoorState
    {
        Closed,
        Opening,
        Open,
        Closing
    }

    [Header("Door Settings")]
    public Mode doorMode;                  // The mode that triggers the door
    public float openHeight = 5f;           // How much the door moves up when it opens
    public float openSpeed = 2f;            // The speed at which the door opens/closes
    public float closeDelay = 1f;           // Delay before the door starts closing
    public DoorState currentDoorState = DoorState.Closed; // Current state of the door

    private Vector3 closedPosition;         // The starting (closed) position of the door
    private Vector3 openPosition;           // The target (open) position of the door
    private Coroutine doorCoroutine;        // Tracks the door's open/close coroutine

    private void Start()
    {
        // Store the door's original position as the closed position
        closedPosition = transform.position;
        // Calculate the open position (move the door up by openHeight)
        openPosition = new Vector3(closedPosition.x, closedPosition.y + openHeight, closedPosition.z);
    }

    /// <summary>
    /// Opens the door by moving it upwards.
    /// </summary>
    [ContextMenu("OpenDoor")]
    public void OpenDoor()
    {
        if (currentDoorState == DoorState.Closed && doorCoroutine == null) // Check if door is not already open
        {
            currentDoorState = DoorState.Opening;  // Set state to Opening
            doorCoroutine = StartCoroutine(MoveDoor(openPosition, DoorState.Open));  // Start moving the door upwards
        }
    }

    /// <summary>
    /// Closes the door by moving it downwards.
    /// </summary>
    [ContextMenu("CloseDoor")]
    public void CloseDoor()
    {
        if (currentDoorState == DoorState.Open && doorCoroutine == null) // Check if door is not already closed
        {
            StartCoroutine(CloseDoorAfterDelay(closeDelay));  // Start closing the door after a delay
        }
    }

    private IEnumerator MoveDoor(Vector3 targetPosition, DoorState endState)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Move the door towards the target position at a constant speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * openSpeed);
            yield return null;
        }

        // Snap to target position to avoid small floating-point inaccuracies
        transform.position = targetPosition;

        // Set the door state to the desired final state (Open or Closed)
        currentDoorState = endState;

        // Reset coroutine when done
        doorCoroutine = null;
    }


    /// <summary>
    /// Starts the door closing process after a delay.
    /// </summary>
    /// <param name="delay">The delay in seconds before the door starts closing.</param>
    private IEnumerator CloseDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the delay
        currentDoorState = DoorState.Closing;    // Set state to Closing
        doorCoroutine = StartCoroutine(MoveDoor(closedPosition, DoorState.Closed));  // Move the door back to the closed position
    }
}
