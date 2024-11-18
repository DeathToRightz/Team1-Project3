using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundMusic;  // The audio source for background music
    public AudioSource randomSound;      // The audio source for the random sound

    [Header("Settings")]
    public float attenuationFactor = 0.2f;  // How much to reduce the background music volume (0 = mute, 1 = no change)
    public float fadeDuration = 0.5f;       // Duration of the fade in seconds

    private float originalBackgroundVolume;

    void Start()
    {
        if (backgroundMusic != null)
        {
            originalBackgroundVolume = backgroundMusic.volume;
        }

        if (randomSound != null)
        {
            randomSound.playOnAwake = false;
            randomSound.loop = false;
            randomSound.clip = null;  // Ensure it's null initially
        }
    }

    public void PlayRandomSound(AudioClip clip)
    {
        if (randomSound == null || clip == null || backgroundMusic == null) return;

        if (!randomSound.isPlaying)
        {
            randomSound.clip = clip;
            StartCoroutine(HandleSoundPlay());
        }
    }

    private IEnumerator HandleSoundPlay()
    {
        // Fade out background music
        yield return StartCoroutine(FadeVolume(backgroundMusic, originalBackgroundVolume * attenuationFactor, fadeDuration));

        // Play the random sound
        randomSound.Play();
        yield return new WaitWhile(() => randomSound.isPlaying);

        // Fade in background music
        yield return StartCoroutine(FadeVolume(backgroundMusic, originalBackgroundVolume, fadeDuration));
    }

    private IEnumerator FadeVolume(AudioSource source, float targetVolume, float duration)
    {
        if (source == null) yield break;

        float startVolume = source.volume;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }
}
