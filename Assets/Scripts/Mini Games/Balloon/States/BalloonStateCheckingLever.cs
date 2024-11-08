using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateCheckingLever : BalloonMiniGamBaseState
{
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is " + this);
        
        OnTransitionState(incomingContext);
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
        CheckIfChosenLeverIsSafe(incomingContext);
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
        throw new System.NotImplementedException();
    }


    bool CheckIfChosenLeverIsSafe(Russian_Balloon incomingContext)
    {
        if(incomingContext.chosenLever.tag == incomingContext.explosiveTagName) { Debug.Log("Player is out"); return false; }
        else return true;
    }
}
