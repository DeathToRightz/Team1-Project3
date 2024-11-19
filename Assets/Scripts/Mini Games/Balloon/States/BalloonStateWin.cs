using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonStateWin : BalloonMiniGamBaseState
{

    public override void OnStartState(Russian_Balloon incomingContext)
    {
        Debug.Log("Current state is " + this);
        PlayerThatDied(incomingContext);
      //incomingContext.StartCoroutine(BalloonPop(incomingContext));

    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
  
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
   
    }


    /*IEnumerator BalloonPop(Russian_Balloon incomingContext)
    {
        incomingContext._balloonAnimator.SetTrigger("Inflate");
        float balloonInflateLength = incomingContext._inflateClip.length;


        yield return new WaitForSeconds(balloonInflateLength);

        Debug.Log(PlayerThatDied(incomingContext));
    }*/

    private void PlayerThatDied(Russian_Balloon incomingContext)
    {
        bool playerChoseBadLever = incomingContext.chosenLever.GetComponent<Lever>().tag == incomingContext.explosiveTagName;

        if(incomingContext.currentPlayerOnStage.name == "FirstPlayer" && playerChoseBadLever) { SceneManager.LoadScene("PlayerTwoWon"); }
        else { SceneManager.LoadScene("PlayerOne Won"); }
    }
}
