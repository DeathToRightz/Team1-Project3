using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalloonStateCheckingLever : BalloonMiniGamBaseState
{
   
    public override void OnStartState(Russian_Balloon incomingContext)
    {
        incomingContext.StartCoroutine(MovePlayerOffStage(incomingContext.currentPlayerOnStage, incomingContext.exitPathPositions, incomingContext)); //Moves Player off the Stage
       
        incomingContext.playerQueue.Enqueue(incomingContext.currentPlayerOnStage); // It will add the current player on stage to the end of the queue
       
        incomingContext.nextPlayer = incomingContext.playerQueue.Peek(); // sets the next player and returns it at the beginning of the queue
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

            incomingContext.OnTransitionState(incomingContext._stateChooseLever);

        }
        else
        {
            Debug.Log("Kaboom");

            incomingContext.OnTransitionState(incomingContext._stateWin);
            }           
        

        if (CheckForAvailableLevers(incomingContext._arrayOfLevers) == 1)
        {

            ResetLevers(incomingContext);
            Debug.Log("One lever left, RESET");

           
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

            if(incomingArray[i].GetComponent<Lever>().leverActive == true)  //Check line 193 of the Russian Ballon Script, there's a comment for you
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

    private void ResetLevers(Russian_Balloon incomingContext)
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
