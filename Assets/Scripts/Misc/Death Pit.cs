using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class DeathPit : MonoBehaviour
{
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
            isGameOver = true;
            cameraZoom.SetWinningPlayer(playerTwo);
            StartCoroutine(ResetLevel());
        }
        if (other.CompareTag("Ball2"))
        {
            isGameOver = true;
            cameraZoom.SetWinningPlayer(playerOne);
            StartCoroutine(ResetLevel());
        }
            
    }


    private void AssingnPlayers()
    {
        GameObject ball1 = GameObject.FindGameObjectWithTag("Ball1");
        GameObject ball2 = GameObject.FindGameObjectWithTag("Ball2");


        if (ball1 != null && ball2 != null)
        {
            playerOne = ball1.transform;
            playerTwo = ball2.transform;
            Debug.Log($"Player One: {playerOne.name}, Player Two: {playerTwo.name}");
        }
        else
        {
            Debug.LogError("Unable to find both players with the tags 'Ball1' and 'Ball2'!");
        }
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(20f);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
