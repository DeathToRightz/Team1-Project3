using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateChooseLever : BalloonMiniGamBaseState
{
    LeverGameManager leverGameManager;
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is: " + this);
        if (incomingContext.chosenLever) { incomingContext.chosenLever = null; }
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
       
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
        if (incomingContext.chosenLever) { incomingContext.chosenLever.GetComponent<Lever>().leverActive = false; incomingContext.OnTransitionState(incomingContext._stateCheckingLever); }
        

    }

    public override void OnLeverSelected(Russian_Balloon incomingContext)
    {
        incomingContext.chosenLever = incomingContext.currentLever;  //ADDED THIS
        incomingContext.OnTransitionState(incomingContext._stateCheckingLever);
    }
}
