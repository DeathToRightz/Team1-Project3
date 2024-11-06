using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Russian_Balloon : MonoBehaviour
{
    [SerializeField] public GameObject[] _arrayOfLevers;
    [SerializeField] public string explosiveTagName;
    BalloonMiniGamBaseState _currentState;
    public BalloonStateChooseLever _stateChooseLever = new BalloonStateChooseLever();
    public BalloonStateRandomizeBadLever _stateRandomizeBadLever = new BalloonStateRandomizeBadLever();


    private void Awake()
    {
        _currentState = _stateRandomizeBadLever;
        _currentState.OnStartState(this);
    }
    public void OnTransitionState(BalloonMiniGamBaseState stateToTransitionTo)
    {
        _currentState.OnTransitionState(this);

        _currentState = stateToTransitionTo;

        _currentState.OnStartState(this);

        Debug.Log("Transitioned to: " + stateToTransitionTo.ToString());
    }

    private void Update()
    {
      //  _currentState.OnUpdateCurrentState(this);
    }
}


