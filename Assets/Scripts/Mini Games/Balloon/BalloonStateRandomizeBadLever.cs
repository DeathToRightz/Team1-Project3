using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateRandomizeBadLever : BalloonMiniGamBaseState
{
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        switch (Random.Range(0, incomingContext._arrayOfLevers.Length))
        {
            case 0:

                incomingContext._arrayOfLevers[0].tag = incomingContext.explosiveTagName;
                break;
            case 1:

                incomingContext._arrayOfLevers[1].tag = incomingContext.explosiveTagName;
                break;
            case 2:

                incomingContext._arrayOfLevers[2].tag = incomingContext.explosiveTagName;
                break;
            case 3:

                incomingContext._arrayOfLevers[3].tag = incomingContext.explosiveTagName;
                break;
            default:
                Debug.LogWarning("Setting up explosive lever went outside of limit");
                break;
        }
        Debug.Log("Lever Randomized");
        incomingContext.OnTransitionState(incomingContext._stateChooseLever);
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
       
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
       
    }
}
