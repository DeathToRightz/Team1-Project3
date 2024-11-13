using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    static FadeScreen _instance;
    private float alphaValueHolder;
    public static FadeScreen instance { get { return _instance; } }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(this.gameObject);

        if(instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
        FadeIn(3);
        SceneManager.activeSceneChanged += FadeInBlackScreen;
    }


    private void FadeInBlackScreen(Scene current, Scene next)
    {
        FadeIn(3);
    }


    public void FadeIn(float fadeDelay)
    {
        StartCoroutine(SetAlpha(0, fadeDelay));
    }

    public void FadeOut(float fadeDelay, bool changeScene,string sceneToChangeTo)
    {
        StartCoroutine(SetAlpha(1, fadeDelay,changeScene,sceneToChangeTo));
    }

    IEnumerator SetAlpha(float desiredAlpha, float fadeDelay, bool changeScene = false, string sceneToChangeTo = null)
    {
      
        alphaValueHolder = Mathf.Clamp(alphaValueHolder,0,1);
        alphaValueHolder = _canvasGroup.alpha;


        while (Mathf.Abs(desiredAlpha - alphaValueHolder) > .0001f)
        {
            alphaValueHolder = Mathf.MoveTowards(alphaValueHolder, desiredAlpha,Time.deltaTime/fadeDelay);
            _canvasGroup.alpha = alphaValueHolder;
            yield return new WaitForEndOfFrame();
        }
        
        if(changeScene ) { yield return new WaitForSeconds(fadeDelay); SceneManager.LoadScene(sceneToChangeTo); }
      
    }

    
}
