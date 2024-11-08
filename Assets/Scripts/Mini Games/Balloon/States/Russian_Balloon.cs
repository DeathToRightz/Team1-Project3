using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Russian_Balloon : MonoBehaviour
{
    //This is the script that hold all of the relevant information need for the Mini Game

    //Array of the levers, may turn into a List
    [SerializeField] public GameObject[] _arrayOfLevers;

    //The tag that is used to define which is the bad lever
    [SerializeField] public string explosiveTagName;

    //The tag that is used to define the safe levers
    [SerializeField] public string safeTagName;

    //Get the reference to the states in the BaseState, all states inherit from the Base State meaning it can be switched between each other
    //causing the state transitions
    BalloonMiniGamBaseState _currentState;

    //Variable of a Choose Lever State that is called here
    public BalloonStateChooseLever _stateChooseLever = new BalloonStateChooseLever();

    //Variable of a Randomize Bad Lever State that is called here
    public BalloonStateRandomizeBadLever _stateRandomizeBadLever = new BalloonStateRandomizeBadLever();


    public BalloonStateCheckingLever _stateCheckingLever = new BalloonStateCheckingLever();

    //Variable of a Randomize Good Lever State that is called here
    //public BallonStateRandomizeGoodLevers _stateRandomizeGoodLever = new BallonStateRandomizeGoodLevers(); // Jancy Added this

    [SerializeField] public GameObject chosenLever = null;
    private void Awake()
    {
        //Current state for game starts as the Randomize Good Lever State
        //_currentState = _stateRandomizeGoodLever; // Jancy Added this

        //Current state for game starts as the Randomize Bad Lever State
        _currentState = _stateRandomizeBadLever;

        //With the current state being switched up top, we call the Start State function is initially made in the Base State
        //TO SEE WHAT THIS DOES move to the BalloonStateRandomizeBadLever script
        _currentState.OnStartState(this); // this refers to this script meaning where ever this is called we have info from here that may be needed else where
    }


    public void OnTransitionState(BalloonMiniGamBaseState stateToTransitionTo) ///When calling this funciton in other states it will ask for the specific state to change to
    {
        //Changes the current state to the chosen one in the parameter,
        //it then starts the Start state funciton for the specific new state, and debugs the name of the new state

        //_currentState.OnTransitionState(this);

        _currentState = stateToTransitionTo;

        _currentState.OnStartState(this);
        
        Debug.Log("Transitioned to: " + stateToTransitionTo.ToString());
    }

    private void Update()
    {
        _currentState.OnUpdateCurrentState(this);
    }
}


