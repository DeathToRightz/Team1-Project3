using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    static FadeScreen _instance;
    private float timer;
    private float alphaValueHolder;
    public static FadeScreen instance { get { return _instance; } }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += FadeInToNewScene;
    }


    private void FadeInToNewScene( Scene current, Scene next)
    {

       // FadeIn(5);
    }


    public void FadeIn(float fadeDelay)
    {
        StartCoroutine(SetAlpha(0,fadeDelay));
    }

    public void FadeOut(float fadeDelay)
    {
        StartCoroutine (SetAlpha(1,fadeDelay));
    }

    IEnumerator SetAlpha(float desiredAlpha, float fadeDelay)
    {
      
        alphaValueHolder = Mathf.Clamp(alphaValueHolder,0,1);
        alphaValueHolder = _canvasGroup.alpha;


        while (Mathf.Abs(desiredAlpha - alphaValueHolder) > .0001f)
        {
            alphaValueHolder = Mathf.MoveTowards(alphaValueHolder, desiredAlpha,Time.deltaTime/fadeDelay);
            _canvasGroup.alpha = alphaValueHolder;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Done");
       // SceneManager.LoadScene("Null");
    }

    
}
