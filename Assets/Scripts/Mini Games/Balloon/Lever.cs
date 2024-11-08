using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField] Transform _position;
    [SerializeField] public LevelOneInput _activePlayer;
    //private LevelOneInput _playerMovementScript;
    private PlayerInput _playerInput;
    private InputAction _chooseLever;
    private Russian_Balloon _balloonScript;
    [SerializeField] public bool leverActive = true;
    private bool _playerInRange = false;
    private void Awake()
    {
        _playerInput = new PlayerInput();
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
        if (leverActive)
        {
            if (Mathf.Abs(_position.position.z - _activePlayer.gameObject.transform.position.z) <= .1) { _balloonScript.chosenLever = this.gameObject; }
        }
        
    }
}
