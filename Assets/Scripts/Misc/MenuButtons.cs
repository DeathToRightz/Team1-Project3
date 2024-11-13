using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    public void OnClickStartButton()
    {
        Debug.Log("Start");
        FadeScreen.instance.FadeOut(3, true, "Level_1_Scene");
    }

    public void OnClickQuitButton()
    {
        StartCoroutine(DelayQuit(3));
    }

    public void OnClickBackButton()
    {
        FadeScreen.instance.FadeOut(3, true, "Main Menu");
    }

    public void OnClickCreditsButton()
    {
        Debug.Log("Credits");
        FadeScreen.instance.FadeOut(3, true, "Credits");
    }

    IEnumerator DelayQuit(float delayFade)
    {
        FadeScreen.instance.FadeOut(delayFade, false, null);
        yield return new WaitForSeconds(delayFade);     
        Application.Quit();
    }
}
