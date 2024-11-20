using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCInputManager : MonoBehaviour
{
    GCInput gcInput;
    PlayerInput playerInput;
    public PlayerInput.LevelThreeActions levelThreeAction;


    private void Awake()
    {
        playerInput = new PlayerInput();
        levelThreeAction = playerInput.LevelThree;
        gcInput = gameObject.GetComponent<GCInput>();
        if (gameObject.tag == "Player1")
        {
            levelThreeAction.PlayerOneMove.performed += ctx => gcInput.SetMovementInput(ctx.ReadValue<Vector2>());
            levelThreeAction.PlayerOneMove.canceled += ctx => gcInput.StopReticle();
        }
        if (gameObject.tag == "Player2")
        {
            levelThreeAction.PlayerTwoMove.performed += ctx => gcInput.SetMovementInput(ctx.ReadValue<Vector2>());
            levelThreeAction.PlayerTwoMove.canceled += ctx => gcInput.StopReticle();
        }
    }



    private void OnEnable()
    {
        levelThreeAction.Enable();
    }

    private void OnDisable()
    {
        levelThreeAction.Disable();
    }
}
