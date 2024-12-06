using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GroceryManager : MonoBehaviour
{
    RoundTranker roundTranker;
    Level3Timer level3Timer;
    LookAtReticle lookAtReticle;
    private bool winnerDetermined = false;
    [SerializeField] public TMP_Text PlayerOneText, PlayerTwoText, winText;
    private bool isGameOver = false;
    private int PlayerOneScore = 0, PlayerTwoScore = 0;
    [SerializeField] public int blockCount;

    private void Awake()
    {
        lookAtReticle = FindAnyObjectByType<LookAtReticle>();
        roundTranker = FindAnyObjectByType<RoundTranker>();
        level3Timer = FindObjectOfType<Level3Timer>();
        // Count all blocks in the scene at the start
        blockCount = GameObject.FindGameObjectsWithTag("Block").Length;
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
        if (!winnerDetermined && (blockCount <= 0 || level3Timer.timeRemaining <= 0f))
        {
            lookAtReticle.canShoot = true;
            DetermineWinner();
        }
    }

    // Method to reduce block count when a block is destroyed
    public void BlockDestroyed()
    {
        blockCount--;
    }

    private void DetermineWinner()
    {
       if (isGameOver) return;

       winnerDetermined = true;
       if (roundTranker.currentRound < 2)
       {
            lookAtReticle.canShoot = false;
            if (PlayerOneScore > PlayerTwoScore)
            {
                roundTranker.OnPlayerWinRound(1);
                EndGame();
                StartCoroutine(ResetNumbers());
            }
            else if (PlayerOneScore < PlayerTwoScore)
            {
                roundTranker.OnPlayerWinRound(2);
                EndGame();
                StartCoroutine(ResetNumbers());
            }
            else if (PlayerOneScore == PlayerTwoScore)
            {
                roundTranker.OnPlayerWinRound(3);
                EndGame();
                StartCoroutine(ResetNumbers());
            }
        }
    }

    public void EndGame()
    {
        if (roundTranker.playerOneWins > 1 || roundTranker.playerTwoWins > 1)
        {
            if (roundTranker.playerOneWins > roundTranker.playerTwoWins)
            {
                winText.text = "Player One Wins";
            }
            else if (roundTranker.playerOneWins < roundTranker.playerTwoWins)
            {
                winText.text = "Player Two Wins";
            }
            FadeScreen.instance.FadeOut(3, true, "Main Menu");
            isGameOver = true;
        }
        else if (roundTranker.playerOneWins > 1 && roundTranker.playerTwoWins > 1 && roundTranker.currentRound == 2)
        {
            TieEndGame();
        }
    }

    public void TieEndGame()
    {
        winText.text = "You guys are Trash. Touch some grass";
        FadeScreen.instance.FadeOut(3, true, "Main Menu");
        isGameOver = true;
    }

    public IEnumerator ResetNumbers()
    {
        if (!isGameOver)
        {
            FadeScreen.instance.FadeOut(1, false, null);
            yield return new WaitForSeconds(4f);
            FadeScreen.instance.FadeIn(3);
            yield return new WaitForSeconds(0.3f);
            level3Timer.timeRemaining = 25;
            winnerDetermined = false;
            PlayerOneScore = 0;
            PlayerTwoScore = 0;
        }
    }
}
