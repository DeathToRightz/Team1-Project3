using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateWin : BalloonMiniGamBaseState
{
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is " + this);
        
        Debug.Log(PlayerThatDied(incomingContext));

    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
        throw new System.NotImplementedException();
    }


    private string PlayerThatDied(Russian_Balloon incomingContext)
    {
        bool playerChoseBadLever = incomingContext.chosenLever.GetComponent<Lever>().tag == incomingContext.explosiveTagName;

        if(incomingContext.currentPlayerOnStage.name == "FirstPlayer" && playerChoseBadLever) { return "Player One ded, so Player Two good"; }
        else { return "Player Two ded, so Player one good"; }
    }
}
