using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeverGameManager : MonoBehaviour
{
    /*
    [SerializeField] private List<GameObject> players; // List of players GameObject

    private int currentPlayerIndex = 0;

    void Start()
    {
        // Randomize player positions at the start
        RandomizePlayerPositions();

        MoveFirstPlayerToPlatform();
    }


    private void RandomizePlayerPositions()
    {
        // Shuffle the player list
        Shuffle(players);

        // Assign eacg player to a random exit position
        for (int i = 0; i < players.Count && i < exitPositions.Length; i++)
        {
            players[i].transform.position = exitPositions[i].position;
            players[i].transform.LookAt(new Vector3(0, players[i].transform.position.y, 0));
        }
    }


    // Fisher-Yates shuffle algorithm to randomize the order of elements in the list
    // This algorithm iterates through the list from the last element to the second element, swapping each element with a randomly chosen element that comes before it (including itself).
    // The result is a list with elements in a random order, ensuring every permutation is equally likely.
    private void Shuffle(List<GameObject> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            // Generate a random index (j) between 0 and i (inclusive)
            int j = Random.Range(0, i + 1);

            // Swap the elements at indices i and j
            GameObject temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public void MovePlayerToExit(GameObject player)
    {
        if (safeLeverCount < exitPositions.Length)
        {
            player.transform.position = exitPositions[safeLeverCount].position;
            player.transform.LookAt(new Vector3(0, player.transform.position.y, 0));
            safeLeverCount++;
        }
        else
        {
            Debug.LogWarning("No more exit positions available.");
        }
    }

    private void MoveFirstPlayerToPlatform()
    {
        if (players.Count > 0 && exitPositions.Length == 0)
        {
            GameObject firstPlayer = players[0];
            LevelOneInput levelOneInput = firstPlayer.GetComponent<LevelOneInput>();

            if (levelOneInput != null)
            {
                levelOneInput.StartMovement();
            }
        }
    }

    private void MovePlayersInSequence()
    {
        while (currentPlayerIndex < players.Count)
        {
            GameObject currentPlayer = players[currentPlayerIndex];
            LevelOneInput levelOneInput = currentPlayer.GetComponent<LevelOneInput>();

            if (levelOneInput != null && !levelOneInput.IsMoving)
            {
                // Move current player to the stage
                levelOneInput.StartMovement();

                // Once player has finished, move them to exit
                StartCoroutine(WaitForPlayerToFinish(levelOneInput));
            }
        }
    }

    private IEnumerator WaitForPlayerToFinish(LevelOneInput levelOneInput)
    {
        while (levelOneInput.IsMoving)
        {
            yield return null;
        }

        currentPlayerIndex++;

        if (currentPlayerIndex < players.Count)
        {
            MovePlayersInSequence();
        }
    }
    */
}
