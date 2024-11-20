using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCInput : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public Vector3 boundaryMin = new Vector3(-10, -10, 0); // Minimum world bounds
    public Vector3 boundaryMax = new Vector3(10, 10, 0); // Maximum world bounds
    private Vector2 movementInput = Vector2.zero; // Current movement input

    public void Update()
    {
        if (movementInput != Vector2.zero)
        {
            MoveReticle(movementInput);
        }
    }
    public void MoveReticle(Vector2 input)
    {
        // Convert input to 3D movement (only X and Y)
        Vector3 movement = new Vector3(input.x, input.y, 0) * moveSpeed * Time.deltaTime;

        // Apply movement
        Vector3 newPosition = transform.position + movement;

        // Clamp position within boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, boundaryMin.x, boundaryMax.x);
        newPosition.y = Mathf.Clamp(newPosition.y, boundaryMin.y, boundaryMax.y);

        // Keep Z position fixed (do not change it)
        newPosition.z = transform.position.z;

        // Update reticle position
        transform.position = newPosition;
    }

    public void StopReticle()
    {
        movementInput = Vector2.zero;  // Stop movement by resetting input
    }

    public void SetMovementInput(Vector2 input)
    {
        movementInput = input;  // Store the movement input from PlayerInputManager
    }

}
