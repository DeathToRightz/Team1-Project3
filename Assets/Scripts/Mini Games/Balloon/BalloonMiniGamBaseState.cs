using UnityEngine;

public abstract class BalloonMiniGamBaseState 
{
    //This is the base for all of the states for the Balloon Mini Game, using a start function, transition function
    // and a Update function. This doesn't attach to anything and is used just for reference for the functions
    public abstract void OnStartState(Russian_Balloon incomingContext);


    public abstract void OnUpdateCurrentState(Russian_Balloon incomingContext);


    public abstract void OnTransitionState(Russian_Balloon incomingContext);
 

}
