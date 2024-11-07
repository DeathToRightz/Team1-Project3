using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneInput : MonoBehaviour
{
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

   /* public void DisableAnimator()
    {
        animator.enabled = false;
    }*/

    private IEnumerator DisableAnimatorAfterAnimation()
    {
        // Wait until the animation has finished playing
        yield return new WaitForSeconds(2.2f); // YOU CAN USE AN EVENT 
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
