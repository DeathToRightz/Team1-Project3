using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField] Transform _position;
    private LevelOneInput _playerMovementScript;
    private PlayerInput _playerInput;
    private InputAction _chooseLever;
    private Russian_Balloon _balloonScript;
    [SerializeField] public bool leverActive = true;
    private void Awake()
    {
       _playerInput = new PlayerInput();
       _playerMovementScript = GameObject.FindObjectOfType<LevelOneInput>();
        _balloonScript = GameObject.FindFirstObjectByType<Russian_Balloon>();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        _chooseLever = _playerInput.LevelOne.ChooseLever;
        _chooseLever.performed += OnLeverPressed;
    }

    private void OnDisable()
    {
        _playerInput?.Disable();
    }

    private void OnLeverPressed(InputAction.CallbackContext context)
    {
        if(Mathf.Approximately(_playerMovementScript.gameObject.transform.position.z,_position.position.z)) {_balloonScript.chosenLever = this.gameObject; }
     
        
    }


    public void TurnOffLever()
    {
        this.leverActive = false;
    }
}
