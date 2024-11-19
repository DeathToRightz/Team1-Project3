using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class DeathPit : MonoBehaviour
{
    [SerializeField] private TMP_Text winText;
    [SerializeField] public bool isGameOver = false;
    [SerializeField] public Transform playerOne;
    [SerializeField] public Transform playerTwo;
    CameraZoom cameraZoom;


    private void Start()
    {
        cameraZoom = FindObjectOfType<CameraZoom>();
        AssingnPlayers();
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Ball1"))
        {
            winText.text = "Karen Wins";
            isGameOver = true;
            cameraZoom.SetWinningPlayer(playerTwo);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Ball2"))
        {
            winText.text = "John Wins"; 
            isGameOver = true;
            cameraZoom.SetWinningPlayer(playerOne);
            other.gameObject.SetActive(false);
        }
            
    }


    private void AssingnPlayers()
    {
        GameObject Karen = GameObject.FindGameObjectWithTag("Ball1");
        GameObject John = GameObject.FindGameObjectWithTag("Ball2");


        if (Karen != null && John != null)
        {
            playerOne = Karen.transform;
            playerTwo = John.transform;
            Debug.Log($"Player One: {playerOne.name}, Player Two: {playerTwo.name}");
        }
        else
        {
            Debug.LogError("Unable to find both players with the tags 'Ball1' and 'Ball2'!");
        }
    }
}
