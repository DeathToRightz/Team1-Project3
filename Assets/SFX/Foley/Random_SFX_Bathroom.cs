using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RandomSFXPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("Add your sound effects here.")]
    public AudioClip[] soundEffects; // Array of sound effects.

    [Tooltip("Minimum time (in seconds) between sound effects.")]
    public float minInterval = 5f;

    [Tooltip("Maximum time (in seconds) between sound effects.")]
    public float maxInterval = 15f;

    [Tooltip("Assign an Audio Mixer Group for routing the audio.")]
    public AudioMixerGroup audioMixerGroup; // Audio mixer group for applying effects.

    private AudioSource audioSource; // AudioSource component.
    private float nextPlayTime;

    void Start()
    {
        if (soundEffects.Length == 0)
        {
            Debug.LogWarning("No sound effects assigned. Please add SFX to the 'soundEffects' array.");
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource component.

        // Assign the Audio Mixer Group if set.
        if (audioMixerGroup != null)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }

        ScheduleNextSFX();
    }

    void Update()
    {
        if (Time.time >= nextPlayTime && soundEffects.Length > 0)
        {
            PlayRandomSFX();
            ScheduleNextSFX();
        }
    }

    private void PlayRandomSFX()
    {
        // Pick a random sound effect.
        int randomIndex = Random.Range(0, soundEffects.Length);
        AudioClip clip = soundEffects[randomIndex];

        // Play the sound effect.
        audioSource.PlayOneShot(clip);
    }

    private void ScheduleNextSFX()
    {
        // Schedule the next sound effect to play.
        nextPlayTime = Time.time + Random.Range(minInterval, maxInterval);
    }
}
