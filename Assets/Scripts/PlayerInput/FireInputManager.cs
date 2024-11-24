using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInputManager : MonoBehaviour
{
    LookAtReticle lookAtReticle;
    PlayerInput playerInput;
    public PlayerInput.LevelThreeActions levelThreeAction;


    private void Awake()
    {
        playerInput = new PlayerInput();
        levelThreeAction = playerInput.LevelThree;
        lookAtReticle = gameObject.GetComponent<LookAtReticle>();
        if (gameObject.tag == "Player1")
        {
            levelThreeAction.PlayerOneFire.performed += ctx => lookAtReticle.ShootCannon();
        }
        if (gameObject.tag == "Player2")
        {
            levelThreeAction.PlayerTwoFire.performed += ctx => lookAtReticle.ShootCannon();
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
