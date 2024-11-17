using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] public GameObject winningPlayer;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float targetFOV = 10f;
    [SerializeField] private float lookAtSpeed = 2f;
    DeathPit deathPit;

    private Camera cam;
    private Transform currentTarget;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<Camera>();
        deathPit = FindObjectOfType<DeathPit>();
    }

    public void ZoomInOnWinner(Transform winner)
    {
        currentTarget = winner;
        StartCoroutine(SmoothZoom(winner));
    }

    private IEnumerator SmoothZoom(Transform winner)
    {
        // Smoothly adjust the field of view to zoom in
        float initialFOV = cam.fieldOfView;
        float timeElapsed = 0f;

        // Zoom in the camera to the target FOV
        while (Mathf.Abs(cam.fieldOfView - targetFOV) > 0.1f)
        {
            timeElapsed += Time.deltaTime;
            cam.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, timeElapsed * zoomSpeed);
            yield return null;
        }

        // Ensure the FOV is set exactly to the target FOV
        cam.fieldOfView = targetFOV;

        // Smoothly rotate to face the player
        Vector3 targetPosition = winner.position;
        while (Vector3.Angle(transform.forward, targetPosition - transform.position) > 0.1f)
        {
            // Calculate direction to the player
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Create the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the camera to look at the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookAtSpeed);
            yield return null;
        }

        // Keep the camera locked on to the player
        while (true)
        {
            if (currentTarget != null && deathPit.isGameOver)
            {
                // Ensure the camera keeps looking at the player (locked on)
                Vector3 direction = (currentTarget.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookAtSpeed);
            }
            yield return null;
        }
    }
}
