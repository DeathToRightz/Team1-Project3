using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject secondSetOfButtons;
    [SerializeField] GameObject firstSetOfButtons;
    [SerializeField] Sprite[] comicsPanels;
    [SerializeField] Canvas _canvas;
    private void Awake()
    {
        Debug.Log(FadeScreen.instance.cutScenePlayed!);
    }
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void OnClickStartButton()
    {
        Debug.Log("Start");
        if (!FadeScreen.instance.cutScenePlayed) { FadeScreen.instance.cutScenePlayed = true; FadeScreen.instance.FadeOut(1, true, "Story Time"); return; }
        ToggleButtons(secondSetOfButtons,true);
        ToggleButtons(firstSetOfButtons,false);      
    }

    public void NextPanel()
    {
        if(_canvas.GetComponent<Image>().sprite == comicsPanels[1]) { FadeScreen.instance.FadeOut(1, true, "Main Menu"); }
        _canvas.GetComponent<Image>().sprite = comicsPanels[1];
        
    }

    public void OnClickQuitButton()
    {
        StartCoroutine(DelayQuit(3));
    }

    public void BackToMainMenu()
    {
        FadeScreen.instance.FadeOut(1, true, "Main Menu");
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
        FadeScreen.instance.FadeOut(1, true, "Credits");
    }

    public void LevelOneLoad()
    {
        Debug.Log("Level 1 help loading");
        FadeScreen.instance.FadeOut(1, true, "Level_1_Help");
    }

    public void LevelTwoLoad()
    {
        Debug.Log("Level 2 help loading");
        FadeScreen.instance.FadeOut(1, true, "Level_2_Help");
    }

    public void LevelThreeLoad()
    {
        Debug.Log("Level 3 help loading");
        FadeScreen.instance.FadeOut(1, true, "Level_3_Help");
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
