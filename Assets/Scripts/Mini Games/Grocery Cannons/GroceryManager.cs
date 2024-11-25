using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroceryManager : MonoBehaviour
{
    Level3Timer level3Timer;
    [SerializeField] public TMP_Text PlayerOneText, PlayerTwoText, winText;
    

    private int blockCount = 0;

    private void Start()
    {
        level3Timer = FindObjectOfType<Level3Timer>();
        // Count all blocks in the scene at the start
        blockCount = GameObject.FindGameObjectsWithTag("Block").Length;
        Debug.Log(blockCount);
    }
    private int PlayerOneScore = 0, PlayerTwoScore = 0;

    // Method to award points to the appropriate player
    public void AwardPoints(string playerTag, int points)
    {
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

        if (blockCount <= 0 || !level3Timer.timerIsRunning)
        {
            if (PlayerOneScore > PlayerTwoScore)
            {
                winText.text = "Player One Wins";
            }
            else if (PlayerOneScore < PlayerTwoScore)
            {
                winText.text = "Player Two Wins";
            }
            else
            {
                winText.text = "Tie";
            }
        }
    }

    // Method to reduce block count when a block is destroyed
    public void BlockDestroyed()
    {
        blockCount--;
    }
}
