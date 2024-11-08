using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeverGameManager : MonoBehaviour
{
    [SerializeField] private Transform[] stagePositions;  // Positions on stage where players move through
    [SerializeField] private Transform[] exitPositions;  // Exit positions for eliminated players
    [SerializeField] private List<GameObject> players;   // List of all players
    public Lever[] _arrayOfLevers;

    private float smoothMoveSpeed = 5f; // Define how fast the players move
    private float smoothMoveThreshold = 0.1f; // Threshold for when to stop moving a player

    private Queue<GameObject> playerQueue = new Queue<GameObject>(); // Queue for players
    private int remainingPlayers; // Number of remaining players
    private bool gameInProgress = true; // Flag to check if the game is still running

    void Start()
    {
        _arrayOfLevers = GameObject.FindObjectsOfType<Lever>();
        RandomizePlayerPositions(); // Shuffle players and set initial positions
        remainingPlayers = players.Count;

        // Enqueue all players at the start
        foreach (var player in players)
        {
            playerQueue.Enqueue(player);
        }

        // Start the game
        StartCoroutine(ProcessQueue());
    }

    // Randomize players and assign them to random exit positions
    private void RandomizePlayerPositions()
    {
        Shuffle(players); // Shuffle players randomly

        for (int i = 0; i < players.Count && i < exitPositions.Length; i++)
        {
            players[i].transform.position = exitPositions[i].position;
            players[i].transform.LookAt(new Vector3(0, players[i].transform.position.y, 0));
        }
    }

    // Fisher-Yates shuffle algorithm to randomize the order of players
    private void Shuffle(List<GameObject> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    // Process the queue where players take turns
    private IEnumerator ProcessQueue()
    {
        while (remainingPlayers > 1 && gameInProgress) // Continue until one player remains
        {
            if (playerQueue.Count == 0) yield break;

            GameObject currentPlayer = playerQueue.Dequeue();
            LevelOneInput levelOneInput = currentPlayer.GetComponent<LevelOneInput>();  // Access LevelOneInput for player actions

            if (levelOneInput != null)
            {
                // Shift the exit positions after a player has moved to the exit
                UpdateExitPositions();
                // Move player through stage positions to the lever area
                yield return StartCoroutine(levelOneInput.MoveThroughStagePositions(stagePositions));

                // Move the player to the first lever position after reaching the stage
                yield return StartCoroutine(levelOneInput.MoveToLeverPoint(levelOneInput.leverSelectionPoints[0])); // Directly use the first point
                levelOneInput.isInStageArea = true;

                // Let the player select a lever using MoveLeft() and MoveRight()
                while (!levelOneInput.IsLeverSelected) // Wait until the lever is selected
                {
                    yield return null; // Continue until the lever is selected
                }

                //for testing purposes
                /*
                bool playerWon = levelOneInput.SelectLever(); // Simulate lever selection

                // If the player wins
                if (playerWon)
                {
                    // Move them to the back of the line (back to their exit position)
                    int exitIndex = Mathf.Min(remainingPlayers - 1, exitPositions.Length - 1);
                    yield return StartCoroutine(levelOneInput.MoveToExit(exitPositions[exitIndex]));

                    // Move the player to the back of the queue
                    playerQueue.Enqueue(currentPlayer);
                }
                else
                {
                    // If the player loses, move them to the exit position (don't destroy the player)
                    int exitIndex = Mathf.Min(remainingPlayers - 1, exitPositions.Length - 1);
                    yield return StartCoroutine(levelOneInput.MoveToExit(exitPositions[exitIndex]));

                    remainingPlayers--;
                }
                */
            }

            yield return new WaitForSeconds(1f); // Wait before the next turn
        }

        // End game when only one player remains
        gameInProgress = false;
        //Debug.Log("Game Over! The last player has won.");
    }

    // Update exit positions by moving players forward
    private void UpdateExitPositions()
    {
        //Debug.Log("Updating exit positions for remaining players.");

        for (int i = 1; i < players.Count; i++)  // Start from the second player in the list
        {
            if (i < exitPositions.Length)
            {
                // Get current player and target exit position
                GameObject player = players[i];
                Transform targetPosition = exitPositions[i - 1];

                StartCoroutine(SmoothMovePlayer(player, targetPosition));
            }
            else
            {
                //Debug.LogError("Player count exceeded exit position count. This shouldn't happen.");
            }
        }

        // Move the last player to the last exit position
        if (players.Count > 0 && players.Count <= exitPositions.Length)
        {
            //Debug.Log($"Moving last player {players[players.Count - 1].name} to the last exit position.");
            GameObject lastPlayer = players[players.Count - 1];
            GameObject firstPlayer = players[players.Count - 3];
            Transform lastExitPosition = exitPositions[exitPositions.Length - 1];

            for(int i = 0; i <= _arrayOfLevers.Length-1; i++) { _arrayOfLevers[i]._activePlayer = firstPlayer.GetComponent<LevelOneInput>(); }

            StartCoroutine(SmoothMovePlayer(lastPlayer, lastExitPosition));
        }
    }

    // Coroutine to smoothly move a player to a new position
    private IEnumerator SmoothMovePlayer(GameObject player, Transform targetPosition)
    {
        // Move the player smoothly using Vector3.MoveTowards to simulate walking
        float journeyLength = Vector3.Distance(player.transform.position, targetPosition.position);
        float startTime = Time.time;

        while (Vector3.Distance(player.transform.position, targetPosition.position) > smoothMoveThreshold)
        {
            float distanceCovered = (Time.time - startTime) * smoothMoveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Move the player towards the target position
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition.position, smoothMoveSpeed * Time.deltaTime);

            yield return null;  // Wait until the next frame
        }

        // Ensure the player is exactly at the target position after moving
        player.transform.position = targetPosition.position;

        //Debug.Log($"Player {player.name} has smoothly moved to the new exit position.");
    }
}
