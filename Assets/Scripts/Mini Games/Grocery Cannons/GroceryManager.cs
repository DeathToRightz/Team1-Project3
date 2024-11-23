using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroceryManager : MonoBehaviour
{
    [SerializeField] public TMP_Text PlayerOneText;
    [SerializeField] public TMP_Text PlayerTwoText;

    private int PlayerOneScore = 0, PlayerTwoScore = 0;
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
}
