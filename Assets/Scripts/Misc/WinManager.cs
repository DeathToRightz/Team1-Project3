using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinManager : MonoBehaviour
{
    [SerializeField] float _delay = 13f;
    void Start()
    {
        FadeScreen.instance.FadeOut(13, true, "Main Menu");
    }

    
 
}
