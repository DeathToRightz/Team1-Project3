using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Default rotation axis
    public float rotationSpeed = 50f; // Default speed in degrees per second

    void Update()
    {
        // Rotate the object continuously around the specified axis and speed
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
