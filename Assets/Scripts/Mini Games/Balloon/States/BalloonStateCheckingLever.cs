using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BalloonStateCheckingLever : BalloonMiniGamBaseState
{
    
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        incomingContext.playerQueue.Peek();
        incomingContext.StartCoroutine(MovePlayerOffStage(incomingContext.currentPlayerOnStage, incomingContext.exitPathPositions, incomingContext));



        //levelOneInput.isInStageArea = false;
        //levelOneInput.isLeverSelected = false;

        //incomingContext.chosenLever = null;
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
        
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
        if (!ChoseBadLever(incomingContext.chosenLever, incomingContext))
        {
            Debug.Log("Safe");
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
        if (incomingObject.tag == incomingContext.explosiveTagName) { Debug.Log("Player is out"); SceneManager.LoadScene(SceneManager.GetActiveScene().name); return true; }
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
        foreach (var position in pathPositions)
        {
            yield return incomingContext.StartCoroutine(incomingContext.SmoothMovePlayer(player, position));
        }

        if (incomingContext.exitPositions.Length > 0)
        {
            yield return incomingContext.StartCoroutine(incomingContext.SmoothMovePlayer(player, incomingContext.exitPositions[0]));
        }
    }
}
