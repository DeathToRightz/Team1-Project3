using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject secondSetOfButtons;
    [SerializeField] GameObject firstSetOfButtons;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void OnClickStartButton()
    {
        Debug.Log("Start");
        ToggleButtons(secondSetOfButtons,true);
        ToggleButtons(firstSetOfButtons,false);
        

       // FadeScreen.instance.FadeOut(3, true, "Level_1_Scene");
    }

    public void OnClickQuitButton()
    {
        StartCoroutine(DelayQuit(3));
    }

    public void OnClickBackButton()
    {
        Debug.Log("Back");
        ToggleButtons(secondSetOfButtons, false);
        ToggleButtons(firstSetOfButtons, true);
        //FadeScreen.instance.FadeOut(3, true, "Main Menu");
    }

    public void OnClickCreditsButton()
    {
        Debug.Log("Credits");
       // FadeScreen.instance.FadeOut(3, true, "Credits");
    }

    public void LevelOneLoad()
    {
        Debug.Log("Level 1 loading");
        FadeScreen.instance.FadeOut(3, true, "Level_1_Help");
    }

    public void LevelTwoLoad()
    {
        Debug.Log("Level 2 loading");
        FadeScreen.instance.FadeOut(3, true, "Level_2_Scene");
    }

    IEnumerator DelayQuit(float delayFade)
    {
        FadeScreen.instance.FadeOut(delayFade, false, null);
        yield return new WaitForSeconds(delayFade);     
        Application.Quit();
    }

    private void ToggleButtons(GameObject incomingObject, bool turnOn)
    {
        int childCount = incomingObject.transform.childCount;
       
        if(turnOn)
        {
            for (int i = 0; i <= childCount-1; i++) { incomingObject.transform.GetChild(i).gameObject.SetActive(true); }
        }
        else
        {
            for (int i = 0; i <= childCount-1; i++) { incomingObject.transform.GetChild(i).gameObject.SetActive(false); }
        }
      
    }
}
