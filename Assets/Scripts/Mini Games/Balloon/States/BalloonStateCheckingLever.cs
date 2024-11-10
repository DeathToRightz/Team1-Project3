using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BalloonStateCheckingLever : BalloonMiniGamBaseState
{
    LevelOneInput levelOneInput;
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        incomingContext.StartCoroutine(MovePlayerOffStage(incomingContext.currentPlayerOnStage, incomingContext.exitPathPositions, incomingContext)); //Moves Player off the Stage
        incomingContext.playerQueue.Enqueue(incomingContext.currentPlayerOnStage); // It will add the current player on stage to the end of the queue
        incomingContext.nextPlayer = incomingContext.playerQueue.Peek(); // sets the next player and retunrs it at the beginning of the queue
        //incomingContext.PlayerToStage();

        //incomingContext.chosenLever = null;
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
        
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
        if (!ChoseBadLever(incomingContext.chosenLever, incomingContext))
        {
            incomingContext.currentPlayerOnStage.GetComponent<LevelOneInput>().isInStageArea = false;
            Debug.Log("Safe");
            //incomingContext.StartCoroutine(incomingContext.ProcessQueue());
            incomingContext.OnTransitionState(incomingContext._stateChooseLever);
            
        }
        else
        {
            
            Debug.Log("Kaboom");
            incomingContext.OnTransitionState(incomingContext._stateWin);
        }

        if (CheckForAvailableLevers(incomingContext._arrayOfLevers) == 1)
        {
            Debug.Log("One lever left, RESET");
            incomingContext.OnTransitionState(incomingContext._stateResetLevers);
        }

    }

    public bool ChoseBadLever(GameObject incomingObject, Russian_Balloon incomingContext)
    {

        if (incomingObject.tag == incomingContext.explosiveTagName) { Debug.Log("Player is out"); incomingContext.remainingPlayers--; return true; }
        else return false;
    }


    private int CheckForAvailableLevers(GameObject[] incomingArray)
    {
        int availableLevers = 0;
        for(int i = 0; i <= incomingArray.Length-1; i++)
        {

            if(incomingArray[i].GetComponent<Lever>().leverActive == true) //Check line 193 of the Russian Ballon Script, there's a comment for you
            {
                availableLevers++;
            }
        }
        Debug.Log(availableLevers);
        return availableLevers;
    }

    private IEnumerator MovePlayerOffStage(GameObject player, Transform[] pathPositions, Russian_Balloon incomingContext)
    {
        //levelOneInput.isInStageArea = false;
        foreach (var position in pathPositions)
        {
            yield return incomingContext.StartCoroutine(incomingContext.SmoothMovePlayer(player, position));
        }

        if (incomingContext.linePositions.Length > 0)
        {
            yield return incomingContext.StartCoroutine(incomingContext.SmoothMovePlayer(player, incomingContext.linePositions[0]));
        }
    }
}
