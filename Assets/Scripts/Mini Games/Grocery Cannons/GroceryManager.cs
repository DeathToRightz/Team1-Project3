using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroceryManager : MonoBehaviour
{
    [SerializeField] public TMP_Text PlayerOneText;
    [SerializeField] public TMP_Text PlayerTwoText;

    private int blockCount = 0;

    private void Start()
    {
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
    }

    // Method to reduce block count when a block is destroyed
    public void BlockDestroyed()
    {
        blockCount--;

        if (blockCount <= 0)
        {
            // You can add logic here for when all blocks are destroyed
            Debug.Log("All blocks destroyed! Game over!");
        }
    }
}
