using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalloonStateCheckingLever : BalloonMiniGamBaseState
{
   
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is " + this);
       
        //incomingContext.chosenLever = null;
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
        
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
       if(!ChoseBadLever(incomingContext.chosenLever,incomingContext)) { incomingContext.OnTransitionState(incomingContext._stateChooseLever); }
    }


    private bool ChoseBadLever(GameObject incomingObject, Russian_Balloon incomingContext)
    {

        if (incomingObject.tag == incomingContext.explosiveTagName) { Debug.Log("Player is out"); return true; }
        else return false;
    }

    private int CheckForAvailableLevers(GameObject[] incomingArray)
    {
        int availableLevers = 0;
        for(int i = 0; i <= incomingArray.Length-1; i++)
        {

            if(incomingArray[i].GetComponent<Lever>().leverActive == true)
            {
                availableLevers++;
            }
        }
        return availableLevers;
    }
}
