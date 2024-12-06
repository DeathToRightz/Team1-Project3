using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    // Detect collision with the player's shot
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RedBall"))
        {
            objectRenderer.material.color = Color.red; //Change color to red
            this.tag = "Player1Block"; // Tag the block for Player 1
            //Destroy(collision.gameObject); // Destroy the shot
        }
        else if (collision.gameObject.CompareTag("BlueBall"))
        {
            objectRenderer.material.color = Color.blue; //Change color to blue
            this.tag = "Player2Block"; // Tag the block for Player 2
            //Destroy(collision.gameObject); // Destroy the shot
        }
    }

}
