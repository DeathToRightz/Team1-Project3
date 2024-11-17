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
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Ball1"))
        {
            isGameOver = true;
            cameraZoom.ZoomInOnWinner(playerTwo);
            StartCoroutine(ResetLevel());
        }
        if (other.CompareTag("Ball2"))
        {
            isGameOver = true;
            cameraZoom.ZoomInOnWinner(playerOne);
            StartCoroutine(ResetLevel());
        }
            
    }


    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(20f);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
