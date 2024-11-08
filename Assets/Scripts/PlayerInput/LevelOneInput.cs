using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneInput : MonoBehaviour
{
    /*
    [SerializeField] private Transform[] leverSelectionPoints; //Array of points to move between
    [SerializeField] private Transform[] stagePoints;
    [SerializeField] private float moveSpeed = 5f;
    private int currentPointIndex = 0; //Current target point

    [SerializeField] private BoxCollider movementArea; //Define a box collider as the allowed movement area
    [SerializeField] private bool shouldMoveToPoint = false; // Controls when MoveToPoint() should be called

    private bool isStageMovement = false;
    [SerializeField] private bool isMoving = false;

    [SerializeField] public bool IsMoving => isMoving;

    void Start()
    {
        // Initial setup (if needed), make sure to check for valid move points
        if (leverSelectionPoints.Length == 0 || stagePoints.Length == 0)
        {
            Debug.LogError("No move points set for player movement.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStageMovement)
        {
            // If in stage movement mode, follow the stage points
            if (IsPlayerInsideMovementArea())
            {
                MoveToStagePoint();
            }
        }
        else
        {
            // If in lever selection mode, follow the lever selection points
            if (IsPlayerInsideMovementArea())
            {
                MoveToLeverSelectionPoint();
            }
        }
    }

    // Move the player towards the target point
    public void MoveToPoint()
    {
        if (leverSelectionPoints.Length == 0 || stagePoints.Length == 0 || isMoving) return;

        // Select the appropriate set of points based on the phase (lever selection or stage movement)
        Transform targetPoint = isStageMovement ? stagePoints[currentPointIndex] : leverSelectionPoints[currentPointIndex];
        StartCoroutine(MovePlayerToPoint(targetPoint));
    }

    private IEnumerator MovePlayerToPoint(Transform targetPoint)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // After reaching the target, switch to the next point or phase
        if (isStageMovement)
        {
            currentPointIndex = (currentPointIndex + 1) % stagePoints.Length; // Move to the next stage point
        }
        else
        {
            currentPointIndex = (currentPointIndex + 1) % leverSelectionPoints.Length; // Move to the next lever point
        }

        isMoving = false;
    }

    // Move to the next lever selection point
    private void MoveToLeverSelectionPoint()
    {
        // Logic for moving to lever selection points
        MoveToPoint();
    }

    // Move to the stage entry/exit point
    private void MoveToStagePoint()
    {
        // Logic for moving to the stage points
        MoveToPoint();
    }

    // Check if the player is inside the defined movement area
    private bool IsPlayerInsideMovementArea()
    {
        return true; // Assuming no bounds check here for simplicity; modify as necessary
    }

    // Allow other scripts to start the movement process
    public void StartLeverSelectionMovement()
    {
        isStageMovement = false; // Set to false for lever selection movement
        currentPointIndex = 0; // Start from the first lever selection point
        MoveToPoint(); // Start moving
    }

    public void StartStageMovement()
    {
        isStageMovement = true; // Set to true for stage movement
        currentPointIndex = 0; // Start from the first stage point
        MoveToPoint(); // Start moving
    }

 

    public void MoveLeft()
    {
        // Only move left if we are not at the first point (index 0)
        if (currentPointIndex > 0)
        {
            currentPointIndex--;
            MoveToPoint(); // Move to the previous point
        }
    }

    // Move right to the next point in the current movement phase (lever or stage)
    public void MoveRight()
    {
        // Only move right if we are not at the last point (index length - 1)
        if (isStageMovement && currentPointIndex < stagePoints.Length - 1)
        {
            currentPointIndex++;
            MoveToPoint(); // Move to the next point in the stage
        }
        else if (!isStageMovement && currentPointIndex < leverSelectionPoints.Length - 1)
        {
            currentPointIndex++;
            MoveToPoint(); // Move to the next lever selection point
        }
    }

    public void StartMovement()
    {
        shouldMoveToPoint = true;
    }
    */


    [SerializeField] private Transform[] movePoints; //Array of points to move between
    [SerializeField] private float moveSpeed = 5f;
    private int currentPointIndex = 0; //Current target point

    [SerializeField] private BoxCollider movementArea; //Define a box collider as the allowed movement area
    [SerializeField] private bool shouldMoveToPoint = false; // Controls when MoveToPoint() should be called

    [SerializeField] private Animator animator;
    [SerializeField] private string animationName;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayAndDisableAnimator();
    }

    public void PlayAndDisableAnimator()
    {
        animator.Play(animationName); // Play the animation
        StartCoroutine(DisableAnimatorAfterAnimation());
    }

    private IEnumerator DisableAnimatorAfterAnimation()
    {
        // Wait until the animation has finished playing
        yield return new WaitForSeconds(2.2f);
        animator.enabled = false; // Disable the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        //Calls MoveToPoint() if shouldMoveToPoint is true
        if (IsPlayerInsideMovementArea())
        {
            MoveToPoint();
            //shouldMoveToPoint = false;
        }
    }

    // Move the player towards the target point
    private void MoveToPoint()
    {
        if (movePoints.Length == 0) return;

        Transform targetPoint = movePoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
    }

    public void MoveLeft()
    {
        // Only move left if we are not at the first point (index 0)
        if (currentPointIndex > 0)
        {
            currentPointIndex--;
            shouldMoveToPoint = true;
        }
    }

    public void MoveRight()
    {
        // Only move right if we are not at the last point (index movePoints.Length - 1)
        if (currentPointIndex < movePoints.Length - 1)
        {
            currentPointIndex++;
            shouldMoveToPoint = true;
        }
    }

    private bool IsPlayerInsideMovementArea()
    {
        return movementArea.bounds.Contains(transform.position);
    }
}
