using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOneInputManager : MonoBehaviour
{
    LevelOneInput levelOneInputScript;
    PlayerInput playerInput;
    public PlayerInput.LevelOneActions levelOneAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        levelOneAction = playerInput.LevelOne;
        levelOneInputScript = gameObject.GetComponent<LevelOneInput>();
        if (gameObject.tag == "Player1")
        {
            levelOneAction.OneMoveLeft.performed += ctx => levelOneInputScript.MoveLeft();
            levelOneAction.OneMoveRight.performed += ctx => levelOneInputScript.MoveRight();
        }
        
        if (gameObject.tag == "Player2")
        {
            levelOneAction.TwoMoveLeft.performed += ctx => levelOneInputScript.MoveLeft();
            levelOneAction.TwoMoveRight.performed += ctx => levelOneInputScript.MoveRight();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        levelOneAction.Enable();
    }

    private void OnDisable()
    {
        levelOneAction.Disable();
    }
}
