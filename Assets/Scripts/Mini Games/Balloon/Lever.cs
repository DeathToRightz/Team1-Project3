using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    LeverGameManager leverGameManager;
    [SerializeField] Transform _position;
    [SerializeField] public LevelOneInput _activePlayer;
    //private LevelOneInput _playerMovementScript;
    private PlayerInput _playerInput;
    private InputAction _chooseLever;
    private Russian_Balloon _balloonScript;
    [SerializeField] public bool leverActive = true;
    private bool _playerInRange = false;
    Russian_Balloon russian_Balloon;

    private void Awake()
    {
         russian_Balloon = FindAnyObjectByType<Russian_Balloon>();
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
            russian_Balloon.SelectLever();
            //if (Mathf.Abs(_position.position.z - _activePlayer.gameObject.transform.position.z) <= .1) { _balloonScript.chosenLever = this.gameObject; _activePlayer.isLeverSelected = true; }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
            russian_Balloon.currentLever = gameObject;
        
    }
}
