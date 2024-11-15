using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInput;

public class BallInputManager : MonoBehaviour
{
    RABInput rollaBallInput;
    PlayerInput playerInput;
    public PlayerInput.LevelTwoActions levelTwoAction;

    private void Awake()
    {
        playerInput = new PlayerInput();
        levelTwoAction = playerInput.LevelTwo;
        rollaBallInput = gameObject.GetComponent<RABInput>();
        if(gameObject.tag == "Ball1")
        {
            levelTwoAction.Player1Move.performed += ctx => rollaBallInput.Move(ctx.ReadValue<Vector2>());
            levelTwoAction.Player1Move.canceled += _ => rollaBallInput.StopMoving();
        }
        else if (gameObject.tag == "Ball2")
        {
            levelTwoAction.Player2Move.performed += ctx => rollaBallInput.Move(ctx.ReadValue<Vector2>());
            levelTwoAction.Player2Move.canceled += _ => rollaBallInput.StopMoving();
        }
    }

    private void OnEnable()
    {
        levelTwoAction.Enable();
    }

    private void OnDisable()
    {
        levelTwoAction.Disable();
    }
}