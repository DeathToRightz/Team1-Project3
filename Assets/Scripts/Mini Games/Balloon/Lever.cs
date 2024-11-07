using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField] Transform _position;
    public LevelOneInput _playerMovementScript;
    private PlayerInput _playerInput;
    private InputAction _chooseLever;
    
    private void Awake()
    {
       _playerInput = new PlayerInput();
       _playerMovementScript = GameObject.FindObjectOfType<LevelOneInput>();
    }
    private void Start()
    {
       Debug.Log(_position.position.z);
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
        if(Mathf.Approximately(_playerMovementScript.gameObject.transform.position.z,_position.position.z)) { Debug.Log(gameObject.name); }
        
        
    }


    public void TurnOffLever()
    {
        gameObject.SetActive(false);
    }
}
