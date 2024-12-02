using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Level3Timer : MonoBehaviour
{
    [SerializeField] public float timeRemaining = 10f;
    //[SerializeField] public bool timerIsRunning = false;
    [SerializeField] public TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        //timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else if (timeRemaining < 0)
        {
            timeRemaining = 0;
          //  timerIsRunning = false;
            Debug.Log("Time over");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(timeToDisplay, 0);

        int seconds = Mathf.FloorToInt(timeToDisplay);

        timerText.text = seconds.ToString();
    }
}