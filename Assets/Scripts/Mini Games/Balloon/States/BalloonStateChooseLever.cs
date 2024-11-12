using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateChooseLever : BalloonMiniGamBaseState
{
    LevelOneInput levelOneInput;
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is: " + this);
        incomingContext.currentPlayerOnStage = incomingContext.playerQueue.Dequeue();
        incomingContext.nextPlayer = incomingContext.playerQueue.Peek();
        levelOneInput = incomingContext.currentPlayerOnStage.GetComponent<LevelOneInput>();
       
        incomingContext.StartCoroutine(MovePlayer(incomingContext));
        
        if (incomingContext.chosenLever) { incomingContext.chosenLever = null;}
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
       
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
             
    }

    public override void OnLeverSelected(Russian_Balloon incomingContext)
    {
        
            incomingContext.chosenLever = incomingContext.currentLever;  //ADDED THIS
        if(incomingContext.chosenLever.GetComponent<Lever>().leverActive)
        {
            incomingContext.chosenLever.GetComponent<Lever>().leverActive = false;
            incomingContext.OnTransitionState(incomingContext._stateCheckingLever);

        }


    }

    public IEnumerator MovePlayer(Russian_Balloon incomingContext)
    {
        yield return new WaitForSeconds(3f);
        yield return levelOneInput.StartCoroutine(levelOneInput.MoveThroughStagePositions(incomingContext.stagePositions));
        yield return levelOneInput.StartCoroutine(levelOneInput.MoveToLeverPoint(levelOneInput.leverSelectionPoints[0]));
    }
}
