using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Russian_Balloon : MonoBehaviour
{
    public GameObject[] _arrayOfLevers;
    
    private void Awake()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void SetUpExplosiveLever(GameObject[] incomingLevers)
    {
        switch (Random.Range(0, _arrayOfLevers.Length))
        {
            case 0:
                Debug.Log("hi");
                break;
            case 1:
                Debug.Log("Bye");
                break;
            default:
                Debug.Log("Wee");
                break;
        }

    }

}


