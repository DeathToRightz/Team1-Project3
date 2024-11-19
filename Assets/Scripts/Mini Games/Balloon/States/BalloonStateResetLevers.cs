using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateResetLevers : BalloonMiniGamBaseState
{
   
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is " + this);
        ResetLevers(incomingContext);
        Debug.Log("Levers Reset");
      
       

        //incomingContext.currentPlayerOnStage = incomingContext.playerQueue.Dequeue();
        //  incomingContext.nextPlayer = incomingContext.playerQueue.Peek();

        incomingContext.OnTransitionState(incomingContext._stateChooseLever);
        
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
       
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
        
    }


    void ResetLevers(Russian_Balloon incomingContext)
    {
        for (int i = 0; i < incomingContext._arrayOfLevers.Length; i++) // Jancy Added this
        {
            incomingContext._arrayOfLevers[i].tag = "Untagged";
        }

        switch (Random.Range(0, incomingContext._arrayOfLevers.Length - 1)) //Jancy added this
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
            case 4:
                incomingContext._arrayOfLevers[4].tag = incomingContext.explosiveTagName;
                break;
            default:
                Debug.LogWarning("Setting up explosive lever went outside of limit");
                break;
        }

        for (int i = 0; i < incomingContext._arrayOfLevers.Length; i++) // Jancy Added this
        {
            if (incomingContext._arrayOfLevers[i].tag != incomingContext.explosiveTagName)
            {
                incomingContext._arrayOfLevers[i].tag = incomingContext.safeTagName;
            }

            incomingContext._arrayOfLevers[i].GetComponent<Lever>().leverActive = true;
        }


    }
}
