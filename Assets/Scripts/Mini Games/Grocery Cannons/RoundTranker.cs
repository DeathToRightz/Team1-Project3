using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundTranker : MonoBehaviour
{
    [SerializeField] private Image[] roundIndicators;
    [SerializeField] private Sprite redCircleSprite, blueCircleSprite, emptyCircleSprite;
    [SerializeField] private int totalRounds = 3;
    [SerializeField] public int currentRound = 0;
    private int[] roundWinners;

    public static RoundTranker instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Otherwise, set this instance
        instance = this;
        roundWinners = new int[totalRounds];
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        LoadRoundData();
        InitializeRoundIndicators();  
        UpdateRoundIndicators();
    }

    private void InitializeRoundIndicators()
    {
         for (int i = 0; i < totalRounds; i++)
        {
            // Load saved winner data for each round, default to 0 if no data exists
            int winner = PlayerPrefs.GetInt("Round" + i, 0); 

            // Set the corresponding round indicator sprite
            if (winner == 1)  // Player 1 wins this round
            {
                roundIndicators[i].sprite = redCircleSprite;
            }
            else if (winner == 2)  // Player 2 wins this round
            {
                roundIndicators[i].sprite = blueCircleSprite;
            }
            else  // No winner for this round yet
            {
                // You can set a neutral state, such as an empty circle sprite
                roundIndicators[i].sprite = null;  // Or set a default empty sprite here
            }
        }
    }

    public void OnPlayerWinRound(int playerID)
    {
        if (currentRound > 2)
        {
            if (playerID == 1)
            {
                roundIndicators[currentRound].sprite = redCircleSprite;
                PlayerPrefs.SetInt("Round" + currentRound, 1);
            }
            else if (playerID == 2)
            {
                roundIndicators[currentRound].sprite = blueCircleSprite;
                PlayerPrefs.SetInt("Round" + currentRound, 2);
            }
            currentRound++;
            PlayerPrefs.Save();
        }
        else
        {
            EndGame();
        }
    }

    public void LoadRoundData()
    {
        for (int i = 0; i < totalRounds; i++)
        {
            roundWinners[i] = PlayerPrefs.GetInt("Round" + i, 0);  // Default to 0 (no winner)
        }
    }

    private void UpdateRoundIndicators()
    {
        for (int i = 0; i < totalRounds; i++)
        {
            int winner = roundWinners[i];
            if (winner == 1)
            {
                roundIndicators[i].sprite = redCircleSprite;  // Player 1 win
            }
            else if (winner == 2)
            {
                roundIndicators[i].sprite = blueCircleSprite; // Player 2 win
            }
        }
    }

    // Method to clear all saved round data after all rounds are over
    private void ClearAllRoundData()
    {
        PlayerPrefs.DeleteAll();  // Delete all PlayerPrefs data (including round data)
        currentRound = 0;         // Reset the round counter
        roundWinners = new int[totalRounds];  // Reset the round winners array
    }

    // Method to delete the save when the game is over and all rounds are finished
    public void EndGame()
    {
        // Clear all round data after the last round
        ClearAllRoundData();
        FadeScreen.instance.FadeOut(3, true, "Main Menu");
    }
}
