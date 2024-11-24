using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointZone : MonoBehaviour
{
    GroceryManager groceryManager;

    void Awake()
    {
        groceryManager = FindObjectOfType<GroceryManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1Block"))
        {
            groceryManager.AwardPoints("Player1", 10); // Award points to Player 1
            Destroy(other.gameObject); // Destroy the block
            groceryManager.BlockDestroyed();
        }
        else if (other.CompareTag("Player2Block"))
        {
            groceryManager.AwardPoints("Player2", 10); // Award points to Player 2
            Destroy(other.gameObject); // Destroy the block
            groceryManager.BlockDestroyed();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
