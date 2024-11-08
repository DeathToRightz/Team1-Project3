using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LevelOneInput : MonoBehaviour
{

    [SerializeField] public Transform[] leverSelectionPoints;  // Define lever selection points on the stage
    [SerializeField] private float moveSpeed = 5f;  // Define movement speed

    private int currentPointIndex = 0; // Current target point for movement
    private bool isMoving = false; // Is the player currently moving
    [SerializeField] public bool isInStageArea = false; // Is the player in the stage area
    private bool isLeverSelected = false; // Has the player selected a lever

    public bool IsMoving => isMoving;  // Property to check if the player is moving
    public bool IsInStageArea => isInStageArea;  // Property to check if the player is in the stage area
    public bool IsLeverSelected => isLeverSelected;  // Check if a lever is selected

    void Start()
    {
        // Optional: Initialize any game-related parameters
    }

    void Update()
    {
        // Update logic if needed (such as checking for player inputs, etc.)
    }


    // Move the player to a specific position (lever or stage position)
    public IEnumerator MoveThroughStagePositions(Transform[] stagePositions)
    {
        isMoving = true;

        for (int i = 0; i < stagePositions.Length; i++)
        {
            while (Vector3.Distance(transform.position, stagePositions[i].position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, stagePositions[i].position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        isMoving = false;
    }

    // Move Left through lever points
    public void MoveLeft()
    {
        if (isInStageArea && !isMoving && currentPointIndex > 0)
        {
            currentPointIndex--;
            StartCoroutine(MoveToLeverPoint(leverSelectionPoints[currentPointIndex]));
        }
    }

    // Move Right through lever points
    public void MoveRight()
    {
        if (isInStageArea && !isMoving && currentPointIndex < leverSelectionPoints.Length - 1)
        {
            currentPointIndex++;
            StartCoroutine(MoveToLeverPoint(leverSelectionPoints[currentPointIndex]));
        }
    }

    // Move player to a lever point
    public IEnumerator MoveToLeverPoint(Transform targetPoint)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    // Simulate lever selection. Randomly determine win or loss
    /*
    public bool SelectLever()
    {
        // This is where the random logic for win/loss happens
        // For example, 50% chance to win
        bool win = Random.value > 0.5f;

        // Mark that a lever has been selected
        isLeverSelected = true;

        return win;
    }
    */

    // Move player to an exit position after game interaction
    public IEnumerator MoveToExit(Transform exitPosition)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, exitPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

}
