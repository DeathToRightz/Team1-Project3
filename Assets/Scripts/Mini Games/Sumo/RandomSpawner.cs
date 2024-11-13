using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private Vector3 platformCenter = new Vector3(0, 1.0f, 0); // Center position of the platform
    [SerializeField] private float minDistance = 3.0f; 
    [SerializeField] private float spawnRadius = 5.0f; // Radius for the spawn area (should match platform radius)

    private void Start()
    {
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        if (playerOne == null || playerTwo == null)
        {
            Debug.LogError("Player references are not assigned in the RandomSpawn script.");
            return;
        }
        Debug.Log("Starting Spawn Player");

        Vector3 positionOne = GetValidSpawnPosition();
        Vector3 positionTwo;

        do
        {
            positionTwo = GetValidSpawnPosition();

        } while (Vector3.Distance(positionOne, positionTwo) < minDistance);

        // Set players' positions
        playerOne.transform.position = positionOne;
        playerTwo.transform.position = positionTwo;

        Debug.Log("Player's have been spawn");
    }

    private Vector3 GetValidSpawnPosition()
    {
        
        Vector3 positon;
        float distanceFromCenter;

        do
        {
            
            positon = SetRandomPosition();
            distanceFromCenter = new Vector3(positon.x, 0, positon.z).magnitude;
        } while (distanceFromCenter > spawnRadius); // Ensure position is within the platform radius

        return positon;
    }

    private Vector3 SetRandomPosition()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float distance = Random.Range(0f, spawnRadius);
        float x = distance * Mathf.Cos(angle);
        float z = distance * Mathf.Sin(angle);

        return new Vector3(platformCenter.x + x, platformCenter.y, platformCenter.z + z); // Position on platform centered on platformCenter
    }

    // Draw the spawn radius as a visual aid in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // Set the color of the circle
        Gizmos.DrawWireSphere(platformCenter, spawnRadius); // Draw a circle with the radius around the object
    }
}
