using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GroceryManager : MonoBehaviour
{
    RoundTranker roundTranker;
    Level3Timer level3Timer;
    [SerializeField] public TMP_Text PlayerOneText, PlayerTwoText, winText;
    private bool isGameOver = false;
    private int PlayerOneScore = 0, PlayerTwoScore = 0;
    private int blockCount;

    private void Awake()
    {
        level3Timer = FindObjectOfType<Level3Timer>();
        // Count all blocks in the scene at the start
        blockCount = GameObject.FindGameObjectsWithTag("Block").Length;
        Debug.Log(blockCount);
    }

    // Method to award points to the appropriate player
    public void AwardPoints(string playerTag, int points)
    {
        if (isGameOver) { return; }
        if (playerTag == "Player1")
        {
            PlayerOneScore += points;
        }
        else if (playerTag == "Player2")
        {
            PlayerTwoScore += points;
        }
    }
    // Update is called once per frame
    void Update()
    {
        PlayerOneText.text = "PlayerOnePoints: " + PlayerOneScore;
        PlayerTwoText.text = "PlayerTwoPoints: " + PlayerTwoScore;

        DetermineWinner(blockCount <= 0 || level3Timer.timeRemaining <= 0f);       
    }

    // Method to reduce block count when a block is destroyed
    public void BlockDestroyed()
    {
        blockCount--;
    }

    private void DetermineWinner(bool gameCondition)
    {
       if (isGameOver) return;

       if(gameCondition)
       {
            if (PlayerOneScore > PlayerTwoScore)
            {
                FindObjectOfType<RoundTranker>().OnPlayerWinRound(1);
                winText.text = "Player One Wins";
                //FadeScreen.instance.FadeOut(3, true, "Main Menu");
                              
            }
            else if (PlayerOneScore < PlayerTwoScore)
            {
                FindObjectOfType<RoundTranker>().OnPlayerWinRound(2);
                winText.text = "Player Two Wins";
                //FadeScreen.instance.FadeOut(3, true, "Main Menu");
                            
            }
           else if (roundTranker.currentRound == 2)
           {
                FadeScreen.instance.FadeOut(3, true, "Main Menu");
                isGameOver = true;              
           }
       }
    }
}
