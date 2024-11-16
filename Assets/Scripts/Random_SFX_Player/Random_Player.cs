using System.Collections;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; // Array to store audio clips
    public AudioSource audioSource; // Reference to the AudioSource component

    [Range(10, 20)]
    public float minInterval = 10f;
    [Range(10, 20)]
    public float maxInterval = 20f;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioClips.Length == 0 || audioSource == null)
        {
            Debug.LogWarning("AudioClips array or AudioSource is not properly set up.");
            return;
        }

        StartCoroutine(PlayRandomAudio());
    }

    private IEnumerator PlayRandomAudio()
    {
        while (true)
        {
            // Play a random clip
            AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.clip = randomClip;
            audioSource.Play();

            // Wait for the clip to finish and an additional random interval
            yield return new WaitForSeconds(randomClip.length + Random.Range(minInterval, maxInterval));
        }
    }
}
