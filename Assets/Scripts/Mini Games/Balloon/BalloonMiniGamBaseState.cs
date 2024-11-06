using UnityEngine;

public abstract class BalloonMiniGamBaseState 
{

    public abstract void OnStartState(Russian_Balloon incomingContext);


    public abstract void OnUpdateCurrentState(Russian_Balloon incomingContext);


    public abstract void OnTransitionState(Russian_Balloon incomingContext);
 

}
