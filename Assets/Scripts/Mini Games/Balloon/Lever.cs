using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField] public Animator leveranim;
    [SerializeField] Transform _position;
    //[SerializeField] public LevelOneInput _activePlayer;
    private LevelOneInput _playerMovementScript;
    private PlayerInput _playerInput;
    private InputAction _chooseLever;
    private Russian_Balloon _balloonScript;
    [SerializeField] public bool leverActive = true;
   // private bool _playerInRange = false;
    Russian_Balloon russian_Balloon;
    
    private string _activePlayerTag = "";

    private void Awake()
    {
         russian_Balloon = FindAnyObjectByType<Russian_Balloon>();
        _playerInput = new PlayerInput();
       // _balloonScript = GameObject.FindFirstObjectByType<Russian_Balloon>();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        ConfigurePlayerInput();
    }

    private void OnDisable()
    {
        _playerInput?.Disable();
    }

    private void ConfigurePlayerInput()
    {
        if (_activePlayerTag == "Player1")
        {
            _chooseLever = _playerInput.LevelOne.OneChooseLever;
            
        }
        else if (_activePlayerTag == "Player2")
        {
            _chooseLever = _playerInput.LevelOne.TwoChooseLever;
        }

        if (_chooseLever != null)
        {
            _chooseLever.performed += OnLeverPressed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            _activePlayerTag = other.tag;

            ConfigurePlayerInput();
            russian_Balloon.currentLever = gameObject;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_activePlayerTag))
        {
            // Reset active player tag when the player exits the range
            _activePlayerTag = "";
            if (_chooseLever != null)
            {
                _chooseLever.performed -= OnLeverPressed;
            }
        }
    }

    private void OnLeverPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Push");
        leveranim.SetBool("isPushed", true);
        Debug.Log("Chose lever");
        russian_Balloon.SelectLever();
    }
}
