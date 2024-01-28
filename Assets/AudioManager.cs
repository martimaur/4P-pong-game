using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioLowPassFilter lowPassFilter;
    private float targetCutoffFrequency = 500.0f;
    private float fadeDuration = 0.6f;
    private float initialCutoffFrequency = 22000.0f;

    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clips -------")]
    public AudioClip bgmusicLoop;
    public AudioClip bgmusicMenu;
    public AudioClip bounce;
    public AudioClip bouncePaddle;
    public AudioClip explosion;
    [Header("--- Power Ups ---")]
    public AudioClip buff;
    public AudioClip debuff;
    public AudioClip ghostBall;
    public AudioClip spikeBall;

    private void Start()
    {
        lowPassFilter = musicSource.GetComponent<AudioLowPassFilter>();
        musicSource.clip = bgmusicLoop;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sound)
    {
        SFXSource.PlayOneShot(sound);
    }

    public void FadeLowPassIn()
    {
        StartCoroutine(FadeLowPass(initialCutoffFrequency, targetCutoffFrequency));
    }

    public void FadeLowPassOut()
    {
        StartCoroutine(FadeLowPass(targetCutoffFrequency, initialCutoffFrequency));
    }

    private System.Collections.IEnumerator FadeLowPass(float startFrequency, float targetFrequency)
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            lowPassFilter.cutoffFrequency = Mathf.Lerp(startFrequency, targetFrequency, elapsedTime / fadeDuration);
            yield return null;
        }

        lowPassFilter.cutoffFrequency = targetFrequency;
    }

}
