using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;


public class RABInput : MonoBehaviour
{
    [SerializeField] private float moveForce = 20f; //Force applied for movement 
    [SerializeField] private float maxSpeed = 5f; //Max rolling speed
    private float drag = 2f; //Deceleration when input is stopped
    [SerializeField] private float bounceForce = 20f;
    private float slipperyFactor = 0.5f;
    private float playerSpeed;
    [SerializeField] private AnimationCurve controlReductionCurve;

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
            // Apply a control reduction factor based on speed
            float controlFactor = controlReductionCurve.Evaluate(playerSpeed / maxSpeed); // Normalized speed
            Vector3 adjustedForce = _moveDirection * moveForce * controlFactor;


            // Apply movement force with speed cap
            if (playerSpeed < maxSpeed)
            {
                _rb.AddForce(_moveDirection * moveForce * slipperyFactor, ForceMode.Acceleration);
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
        if (playerSpeed > 0)
        {
            _moveDirection = Vector3.zero;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball1") || collision.gameObject.CompareTag("Ball2"))
        {
            //Debug.Log("Players Have collided");
            // Apply bounce force upon collision with aother player 
            Vector3 bounceDirection = (collision.transform.position - transform.position); // Normal direction of contact

            bounceDirection.y = 0;
            bounceDirection.Normalize();

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

    private void OnValidate()
    {
        if (controlReductionCurve == null || controlReductionCurve.length == 0)
        {
            // Initialize a default curve if none is set
            controlReductionCurve = new AnimationCurve(
                new Keyframe(0f, 1f),  // Full control at low speed
                new Keyframe(1f, 0.4f) // Reduced control at max speed
            );
        }
    }


    /*
   public void AddUpdaForce()
   {
       moveForce += Mathf.MoveTowards(moveForce, 10, Time.deltaTime);
   }
   */
}
