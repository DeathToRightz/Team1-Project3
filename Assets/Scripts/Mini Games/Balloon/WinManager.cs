using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinManager : MonoBehaviour
{
    
    void Start()
    {
        FadeScreen.instance.FadeOut(5, true, "Main Menu");
    }

    
 
}
