using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    [Header("Music Tracks")]
    [SerializeField]
    AudioSource[] music;

    [Header("Player Sound Effects")]
    [SerializeField]
    AudioSource[] playerSFX;

    [Header("Object Sound Effects")]
    [SerializeField]
    AudioSource[] objectSFX;

    [Header("Menu Sound Effects")]
    [SerializeField]
    AudioSource[] menuSFX;

    // Getters
    public AudioSource[] GetMusic() { return music; }
    public AudioSource[] GetPlayerSFX() { return playerSFX; }
    public AudioSource[] GetObjectSFX() { return objectSFX; }  
    public AudioSource[] GetMenuSFX() { return menuSFX; }
}
