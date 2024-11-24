using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] AudioClip _screamSFX;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Invoke("PlayScream", 1.7f);
        Destroy(gameObject, 6);
    }

    private void Update()
    {
        StartCoroutine(LowerScream());
    }


    IEnumerator  LowerScream()
    {
        yield return new WaitForSeconds(1.8f);
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0, Time.deltaTime / 3);
    }

    private void PlayScream()
    {
        _audioSource.PlayOneShot(_screamSFX);
    }
}
