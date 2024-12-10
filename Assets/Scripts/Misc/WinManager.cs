using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinManager : MonoBehaviour
{
    [SerializeField] float _delay = 0f;

    void Start()
    {
        Debug.Log("Changing");
        FadeScreen.instance.FadeOut(_delay, true, "Main Menu");
    }

    
 
}
