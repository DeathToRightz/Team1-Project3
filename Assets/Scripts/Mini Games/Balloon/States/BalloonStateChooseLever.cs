using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateChooseLever : BalloonMiniGamBaseState
{
    LeverGameManager leverGameManager;
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is: " + this);
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
       
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
         
        if(incomingContext.chosenLever && incomingContext.chosenLever.tag == incomingContext.explosiveTagName) { Debug.Log("Kaboom"); }
        if(incomingContext.chosenLever && incomingContext.chosenLever.tag == incomingContext.safeTagName) 
        { 
            Debug.Log("safe");
            /*
            GameObject player = incomingContext.gameObject;

            if (leverGameManager != null && player != null)
            {
                //leverGameManager.MovePlayerToExit(player);
            }
            */
        }
    }
}
