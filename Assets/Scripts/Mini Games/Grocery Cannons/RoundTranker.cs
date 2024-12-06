using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundTranker : MonoBehaviour
{
    GroceryManager groceryManager;
    [SerializeField] private GameObject[] roundObjects;
    [SerializeField] private Image[] roundIndicators;
    [SerializeField] private Sprite redCircleSprite, blueCircleSprite, emptyCircleSprite;
    [SerializeField] public int currentRound = 0;

    [SerializeField] public int playerOneWins = 0;
    [SerializeField] public int playerTwoWins = 0;

    private void Start()
    {
        groceryManager = FindAnyObjectByType<GroceryManager>();
        ResetRounds();
        UpdateRoundVisibility();
    }

    public void OnPlayerWinRound(int playerID)
    {
            if (playerID == 1)
            {
                roundIndicators[currentRound].sprite = redCircleSprite;
                playerOneWins++;
            }
            else if (playerID == 2)
            {
                roundIndicators[currentRound].sprite = blueCircleSprite;
                playerTwoWins++;
            }
            else if (playerID == 3)
            {
                roundIndicators[currentRound].sprite = emptyCircleSprite;
            }
            currentRound++;
            UpdateRoundVisibility();  // Update visibility for the next round

    }

    private void UpdateRoundVisibility()
    {
        for (int i = 0; i < roundObjects.Length; i++)
        {
            roundObjects[i].SetActive(i == currentRound);
        }
    }
    public void ResetRounds()
    {
        for (int i = 0; i < roundIndicators.Length; i++)
        {
            roundIndicators[i].sprite = emptyCircleSprite;  // Set all indicators to empty
        }

        currentRound = 0;  // Reset round counter
        UpdateRoundVisibility();  // Show the first round's GameObject
    }
   
}
