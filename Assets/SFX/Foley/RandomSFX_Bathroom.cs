using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class RandomSFXPlayer : MonoBehaviour
{
    [System.Serializable]
    public class SFX
    {
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    public List<SFX> sfxList = new List<SFX>();
    public AudioMixerGroup audioMixerGroup;
    public float playInterval = 5f;

    private AudioSource audioSource;
    private float timer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioMixerGroup != null)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= playInterval && sfxList.Count > 0)
        {
            PlayRandomSFX();
            timer = 0f;
        }
    }

    private void PlayRandomSFX()
    {
        int randomIndex = Random.Range(0, sfxList.Count);
        SFX randomSFX = sfxList[randomIndex];

        audioSource.PlayOneShot(randomSFX.clip, randomSFX.volume);
    }
}
