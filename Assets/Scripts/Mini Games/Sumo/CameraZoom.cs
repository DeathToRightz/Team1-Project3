using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float zoomSpeed = 1.5f;
    private float targetFOV = 14f;

    private Transform winningPlayer;
    DeathPit deathPit;

    
    // Start is called before the first frame update
    void Awake()
    {
        virtualCamera.m_Lens.FieldOfView = 60f;
        deathPit = FindObjectOfType<DeathPit>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void SetWinningPlayer(Transform winner)
    {
        if (deathPit.isGameOver)
        {
            winningPlayer = winner;

            // Set the Look At property of the Cinemachine camera to the winner
            virtualCamera.LookAt = winningPlayer;

            // Start zooming the camera
            StartCoroutine(ZoomIn());
        }
    }

    private IEnumerator ZoomIn()
    {
        float initialFOV = virtualCamera.m_Lens.FieldOfView;

        FadeScreen.instance.FadeOut(5f, true, "Main Menu");

        // Smoothly adjust the FOV
        while (Mathf.Abs(virtualCamera.m_Lens.FieldOfView - targetFOV) > 0.1f)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        // Ensure FOV is exactly at the target
        virtualCamera.m_Lens.FieldOfView = targetFOV;
    }

    
}
