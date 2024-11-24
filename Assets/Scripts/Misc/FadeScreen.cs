using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    static FadeScreen _instance;
    private float alphaValueHolder;
    private bool _alphaChanging = false;
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
        FadeIn(1);
    }


    public void FadeIn(float fadeDelay)
    {
        if (!_alphaChanging){ StartCoroutine(SetAlpha(0, fadeDelay)); }
        
    }

    public void FadeOut(float fadeDelay, bool changeScene,string sceneToChangeTo)
    {

        if (!_alphaChanging){ StartCoroutine(SetAlpha(1, fadeDelay, changeScene, sceneToChangeTo)); } 
    }

    IEnumerator SetAlpha(float desiredAlpha, float fadeDelay, bool changeScene = false, string sceneToChangeTo = null)
    {
      
        alphaValueHolder = Mathf.Clamp(alphaValueHolder,0,1);
        alphaValueHolder = _canvasGroup.alpha;


        while (Mathf.Abs(desiredAlpha - alphaValueHolder) > .0001f)
        {
            _alphaChanging = true;
            alphaValueHolder = Mathf.MoveTowards(alphaValueHolder, desiredAlpha,Time.deltaTime/fadeDelay);
            _canvasGroup.alpha = alphaValueHolder;
            yield return new WaitForEndOfFrame();
        }
        _alphaChanging = false;
        if(changeScene ) { yield return new WaitForSeconds(3); SceneManager.LoadScene(sceneToChangeTo); }
      
    }

    
}
