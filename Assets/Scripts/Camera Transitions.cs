using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
public class CameraTransitions : MonoBehaviour
{
    [SerializeField]GameObject[] cameras;


    private void Start()
    {
       
        StartCoroutine(ChangeCameraView(cameras));
    }


    IEnumerator ChangeCameraView(GameObject[] incomingArray)
    {
       int i = 0;
            while (i <= incomingArray.Length-1 )
            {
                yield return new WaitForSeconds(4);
                Debug.Log("Switch" + i);
                incomingArray[i].SetActive(false);
            i++;
            }
            
        
        Debug.Log("Finish");
        incomingArray[0].SetActive(true);
    }
}
