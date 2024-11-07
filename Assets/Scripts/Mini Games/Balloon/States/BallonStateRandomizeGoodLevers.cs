using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonStateRandomizeGoodLevers : BalloonMiniGamBaseState
{
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is: " + this);

        //Set all other levers to "safe"
        for (int i = 0; i < incomingContext._arrayOfLevers.Length; i++)
        {
            if (incomingContext._arrayOfLevers[i].tag != incomingContext.explosiveTagName)
            {
                incomingContext._arrayOfLevers[i].tag = incomingContext.safeTagName;
            }
        }
        incomingContext.OnTransitionState(incomingContext._stateChooseLever);
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
        // Implement any transition logic if needed
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
        // Implement any update logic if needed
    }


}
