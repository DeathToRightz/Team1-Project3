using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private List<string> titleNames = new List<string>();

    private void Start()
    {
        selectedNames();
        randomSelectTitle();
    }


    private void selectedNames()
    {
        titleNames.Add("Rude Mart");
        titleNames.Add("Purchase & Punishment");
        titleNames.Add("Unreasonable Aisles");
        titleNames.Add("The Final Refund");
        titleNames.Add("Grizzler Mart");
        titleNames.Add("Purchase & Punishment");
    }

    private void randomSelectTitle()
    {
        int titleIndex = Random.Range(0, titleNames.Count);
        string selectedTitle = titleNames[titleIndex];

        titleText.text = selectedTitle;
        if (titleNames[titleIndex] == null) { Debug.Log("No titles where added"); }
    }
}
