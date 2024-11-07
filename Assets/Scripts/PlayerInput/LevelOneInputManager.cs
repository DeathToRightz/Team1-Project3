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

        levelOneAction.MoveLeft.performed += ctx => levelOneInputScript.MoveLeft();
        levelOneAction.MoveRight.performed += ctx => levelOneInputScript.MoveRight();    
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
