using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;


public class RABInput : MonoBehaviour
{
    private float moveForce = 10f; //Force applied for movement 
    [SerializeField] private float maxSpeed = 5f; //Max rolling speed
    private float drag = 2f; //Deceleration when input is stopped
    [SerializeField] private float dampingFactor = 0.95f;
    [SerializeField] private float bounceForce = 5f;
    [SerializeField] private float playerSpeed;

    private Rigidbody _rb;
    private Vector3 _moveDirection;
    private Collider _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.drag = drag;
    }

    private void FixedUpdate()
    {
        playerSpeed = _rb.velocity.magnitude;
        if (_moveDirection != Vector3.zero)
        {
            // Apply movement force with speed cap
            if (playerSpeed < maxSpeed)
            {
                _rb.AddForce(_moveDirection * moveForce, ForceMode.Acceleration);
            }
        }
    }

    public void Move(Vector2 input)
    {
        // Convert 2d input into 3D movement direction for XZ plane
        _moveDirection = input != Vector2.zero ? new Vector3(input.x, 0, input.y).normalized : Vector3.zero;

    }

    public void StopMoving()
    {
        _moveDirection = Vector3.zero;
        //Mathf.Clamp(moveForce, 0.1f, 10f);
        //moveForce -= Mathf.MoveTowards(moveForce, 0, Time.deltaTime);
    }

    public void AddUpdaForce()
    {
        moveForce += Mathf.MoveTowards(moveForce, 10, Time.deltaTime);
    }


    private void ApplyRapidStop()
    {
            Debug.Log("Stoping");
        // Apply counter force to rapidly stop the player
        _rb.velocity *= dampingFactor;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball1") || collision.gameObject.CompareTag("Ball2"))
        {
            //Debug.Log("Players Have collided");
            // Apply bounce force upon collision with aother player 
            Vector3 bounceDirection = collision.contacts[0].normal; // Normal direction of contact
            _rb.AddForce(-bounceDirection * bounceForce, ForceMode.Impulse); // Apply the bounce force

            // Apply bounce force to the other player by get the Rigidbody of the other player
            Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                // Apply force to the other player's rigidbody
                otherRb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Never Found other Rigidbody");
            }
        }
    }
}
