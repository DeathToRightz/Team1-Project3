using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateChooseLever : BalloonMiniGamBaseState
{
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is: " + this);
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
        throw new System.NotImplementedException();
    }
}
