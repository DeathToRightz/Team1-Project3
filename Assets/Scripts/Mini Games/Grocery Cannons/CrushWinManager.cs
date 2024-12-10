using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushWinManager : MonoBehaviour
{
    [SerializeField] Animator crushedAnim;

    private AudioSource _audioSource;

    [SerializeField] AudioClip squishSFX;

    private void Awake()
    {
        crushedAnim = GameObject.FindObjectOfType<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        StartCoroutine(CrushAnim());
    }

    IEnumerator CrushAnim()
    {
        yield return new WaitForSeconds(1.55f);
        //Debug.Log("Player Crushed");
        crushedAnim.SetTrigger("isCrushed");
        _audioSource.PlayOneShot(squishSFX);
        yield return new WaitForSeconds(5f);
        Debug.Log("Changing");
        FadeScreen.instance.FadeOut(2f, true, "Main Menu");
    }
}
