using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneInput : MonoBehaviour
{

    [SerializeField] public Transform[] leverSelectionPoints;  // Define lever selection points on the stage
    [SerializeField] private float moveSpeed = 5f;  // Define movement speed
    [SerializeField] private Animator animator;
    private float positionRotationSpeed = 5f;
    private float stageRotationSpeed = 20f;

    private int currentPointIndex = 0; // Current target point for movement
    private bool isMoving = false; // Is the player currently moving
    [SerializeField] public bool isInStageArea = false; // Is the player in the stage area
    [SerializeField] public bool isLeverSelected = false; // Has the player selected a lever
    private Rigidbody rb;

    public bool IsMoving => isMoving;  // Property to check if the player is moving
    public bool IsInStageArea => isInStageArea;  // Property to check if the player is in the stage area
    public bool IsLeverSelected => isLeverSelected;  // Check if a lever is selected

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Update logic if needed (such as checking for player inputs, etc.)
        if (isInStageArea && !isMoving)
        {
            RotateCharacter(90f);
        }
        else if (!isInStageArea && !isMoving) // This causes the snap after selecting lever
        {
            RotateCharacter(-90f);
        }

        if (isMoving)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }


    // Move the player to a specific position (lever or stage position)
    public IEnumerator MoveThroughStagePositions(Transform[] stagePositions)
    {
       // currentPointIndex = 0;
        isMoving = true;
        
        for (int i = 0; i < stagePositions.Length; i++)
        {
            while (Vector3.Distance(transform.position, stagePositions[i].position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, stagePositions[i].position, moveSpeed * Time.deltaTime);

              RotateTowardsTarget(stagePositions[i]);

                yield return null;
            }
        }

        isMoving = false;
    }
   



    // Move Left through lever points
    public void MoveLeft()
    {

        if (isInStageArea && !isMoving && currentPointIndex > 0)
        {
            currentPointIndex--;
            StartCoroutine(MoveToLeverPoint(leverSelectionPoints[currentPointIndex]));
            RotateCharacter(0f);

        }
    }

    // Move Right through lever points
    public void MoveRight()
    {
        if (isInStageArea && !isMoving && currentPointIndex < leverSelectionPoints.Length - 1)
        {
            currentPointIndex++;
            StartCoroutine(MoveToLeverPoint(leverSelectionPoints[currentPointIndex]));
            RotateCharacter(-180f);
        }
    }

    // Move player to a lever point
    public IEnumerator MoveToLeverPoint(Transform targetPoint)
    {
        isInStageArea = true;
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }


    // Move player to an exit position after game interaction
    public IEnumerator MoveToExit(Transform exitPosition)
    {
        
        isMoving = true;

         while (Vector3.Distance(transform.position, exitPosition.position) > 0.1f)
         {
              transform.position = Vector3.MoveTowards(transform.position, exitPosition.position, moveSpeed * Time.deltaTime);

             RotateTowardsTarget(exitPosition);

             yield return null;
         }
        

        isMoving = false;
    }

    // Rotate the player towards the target position
    public void RotateTowardsTarget(Transform target)
    {
        // Calculate the rotation step
        Vector3 direction = (target.position - transform.position).normalized;
       
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Smoothly rotate towards the target using rotationSpeed
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, positionRotationSpeed * Time.deltaTime);
    }

    public void RotateCharacter(float targetRotation)
    {
        if (isInStageArea && !isMoving)
        {
            // Get the current rotation in Euler angles
            Vector3 currentRotation = transform.eulerAngles;

            // Smoothly rotate to the target rotation on the Y axis, locking the X and Z rotation
            float newRotationY = Mathf.LerpAngle(currentRotation.y, targetRotation, stageRotationSpeed * Time.deltaTime);

            // Apply the new rotation while keeping X and Z axis unchanged
            transform.rotation = Quaternion.Euler(currentRotation.x, newRotationY, currentRotation.z);
           
        }
        else
        {
            Vector3 currentRotation = transform.eulerAngles;

            // Snap instantly to the target rotation by setting the rotation directly
            transform.rotation = Quaternion.Euler(currentRotation.x, targetRotation, currentRotation.z);
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Exit Stage Pos3")
        {
            Debug.Log("Off stage");
            isInStageArea = false;
        }
    }

}
