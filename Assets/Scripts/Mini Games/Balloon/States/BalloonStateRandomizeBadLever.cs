using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Randomize Bad Lever State that inherits from the Base State, using the functions defined there
//But changing what in them
public class BalloonStateRandomizeBadLever : BalloonMiniGamBaseState
{
    //Starts by debugging the name of the current active state
    //Then out of the array of levers from the Russian Balloon state choosing which one of them to give the Danger Tag
    //Afterwards will automatically change to the choose lever state
    public override void OnStartState(Russian_Balloon incomingContext)
    {
       Debug.Log("Current state is: " + this);

        // Ramdomly selects an index for the "bad" level
        int badLeverIndex = Random.Range(0, incomingContext._arrayOfLevers.Length); //Jancy added this

        // Sets the "bad" lever based on the badLeverIndex
        switch (badLeverIndex) //Jancy added this
        {
            case 0:
                Debug.Log("lever 1 set");
                incomingContext._arrayOfLevers[0].tag = incomingContext.explosiveTagName;
                break;
            case 1:
                Debug.Log("lever 2 set");
                incomingContext._arrayOfLevers[1].tag = incomingContext.explosiveTagName;
                break;
            case 2:
                Debug.Log("lever 3 set");
                incomingContext._arrayOfLevers[2].tag = incomingContext.explosiveTagName;
                break;
            case 3:
                Debug.Log("lever 4 set");
                incomingContext._arrayOfLevers[3].tag = incomingContext.explosiveTagName;
                break;
            case 4:
                Debug.Log("lever 5 set");
                incomingContext._arrayOfLevers[4].tag = incomingContext.explosiveTagName;
                break;
            case 5:
                incomingContext._arrayOfLevers[5].tag = incomingContext.explosiveTagName;
                break;
        }

        //Set all other levers to "safe"
        for (int i = 0; i < incomingContext._arrayOfLevers.Length; i++) // Jancy Added this
        {
            if (incomingContext._arrayOfLevers[i].tag != incomingContext.explosiveTagName)
            {
                incomingContext._arrayOfLevers[i].tag = incomingContext.safeTagName;
            }
        }

        //Calls the Russian Balloon reference we get from the paramater and switches to the choose state lever
        incomingContext.OnTransitionState(incomingContext._stateChooseLever);
    }

    public override void OnTransitionState(Russian_Balloon incomingContext)
    {
       
    }

    public override void OnUpdateCurrentState(Russian_Balloon incomingContext)
    {
       
    }
}
