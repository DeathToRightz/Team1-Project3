using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateWin : BalloonMiniGamBaseState
{

    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is " + this);
        
      incomingContext.StartCoroutine(BalloonPop(incomingContext));

    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
  
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
   
    }


    IEnumerator BalloonPop(Russian_Balloon incomingContext)
    {
        incomingContext._balloonAnimator.SetTrigger("Inflate");
        float balloonInflateLength = incomingContext._inflateClip.length;


        yield return new WaitForSeconds(balloonInflateLength);

        Debug.Log(PlayerThatDied(incomingContext));
    }

    private string PlayerThatDied(Russian_Balloon incomingContext)
    {
        bool playerChoseBadLever = incomingContext.chosenLever.GetComponent<Lever>().tag == incomingContext.explosiveTagName;

        if(incomingContext.currentPlayerOnStage.name == "FirstPlayer" && playerChoseBadLever) { return "Player One ded, so Player Two good"; }
        else { return "Player Two ded, so Player one good"; }
    }
}
