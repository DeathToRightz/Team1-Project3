using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HelpNext : MonoBehaviour
{
  public void StartGame(string incomingString)
    {
       Debug.Log("Loading Game");
       FadeScreen.instance.FadeOut(3,true, incomingString);
    }
}
