using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(AudioHolder))]
public class AudioManager : Singleton<AudioManager>
{
    AudioHolder holder;

    [SerializeField]
    AudioSource currentMusic;

    private void Awake()
    {
        // get Audio Holder reference
        holder = GetComponent<AudioHolder>();
    }

    public AudioSource GetSound(int arrayNumber, int index)
    {
        switch (arrayNumber)
        {
            case 0:
                return holder.GetMusic()[index];
            case 1:
                return holder.GetPlayerSFX()[index];
            case 2:
                return holder.GetObjectSFX()[index];
            case 3:
                return holder.GetMenuSFX()[index];
            default:
                return null;
        } 
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Play();
    }

    public void PlaySound(int arrayNumber, int index)
    {
        GetSound(arrayNumber, index).Play();
    }

    public void StopSound(AudioSource sound)
    {
        sound.Stop();
    }

    /// <summary>
    /// Plays a new track from the music array without stopping previous track
    /// </summary>
    /// <param name="track"></param>
    public void PlayMusic(AudioSource track)
    {
        currentMusic = track;
        currentMusic.Play();
    }

    /// <summary>
    /// Plays a new track from the music array without stopping previous track. takes in a fade in duration value
    /// </summary>
    /// <param name="track"></param>
    /// <param name="fadeInDuration"></param>
    public void PlayMusic(AudioSource track, float fadeInDuration)
    {
        currentMusic = track;
        currentMusic.Play();
        StartCoroutine(FadeInSound(currentMusic, fadeInDuration));
    }

    /// <summary>
    /// Stops current music from playing and sets new track to play from the music array
    /// </summary>
    /// <param name="track"></param>
    public void StopPlayMusic(AudioSource track)
    {
        currentMusic.Stop();
        currentMusic = track;
        currentMusic.Play();
    }

    /// <summary>
    /// Stops current music from playing and sets new track to play from the music array with fade out duration
    /// </summary>
    /// <param name="track"></param>
    public void StopPlayMusic(AudioSource track, float fadeDuration)
    {
        StartCoroutine(FadeOutSound(currentMusic, fadeDuration));
        currentMusic = track;
        currentMusic.Play();
    }

    /// <summary>
    /// Crossfades two tracks with fade in and fade out duration
    /// </summary>
    /// <param name="track"></param>
    /// <param name="fadeOutDuration"></param>
    /// <param name="fadeInDuration"></param>
    public void StopPlayMusic(AudioSource track, float fadeOutDuration, float fadeInDuration)
    {
        StartCoroutine(FadeOutSound(currentMusic, fadeOutDuration));
        currentMusic = track;
        currentMusic.Play();
        StartCoroutine(FadeInSound(currentMusic, fadeInDuration));
    }

    /// <summary>
    /// Stops current music track from playing with fadeout time.
    /// </summary>
    /// <param name="fadeDuration"></param>
    public void StopMusic(float fadeDuration) 
    {
        StartCoroutine(FadeOutSound(currentMusic, fadeDuration));
    }

    /// <summary>
    /// Plays a randomly selected sound from specified array
    /// </summary>
    /// <param name="range"></param>
    public void PlayRandomSound(int arrayNumber, int rangeMin, int rangeMax)
    {
        int random = UnityEngine.Random.Range(rangeMin, rangeMax +1);
        Debug.Log(random);
        GetSound(arrayNumber, random).Play();
    }

    // gets currently playing track
    public AudioSource GetCurrentTrack()
    {
        return currentMusic;
    }


    IEnumerator FadeOutSound(AudioSource sound, float duration)
    {
        float currentTime = 0;
        float start = sound.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            sound.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }

        sound.Stop();
        yield break;
    }

    IEnumerator FadeInSound(AudioSource sound, float duration)
    {
        float currentTime = 0;
        float goal = sound.volume;
        sound.volume = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            sound.volume = Mathf.Lerp(0, goal, currentTime / duration);
            yield return null;
        }

        yield break;
    }
}
