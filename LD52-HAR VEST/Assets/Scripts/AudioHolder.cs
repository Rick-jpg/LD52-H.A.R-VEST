using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour 
{
    [Header("Music Tracks")]
    [SerializeField]
    AudioSource[] music;

    [Header("Sound Effects")]
    [SerializeField]
    AudioSource[] playerSFX;
    [SerializeField]
    AudioSource[] objectSFX;
    [SerializeField]
    AudioSource[] menuSFX;

    // Getters
    public AudioSource[] GetMusic() { return music; }
    public AudioSource[] GetPlayerSFX() { return playerSFX; }
    public AudioSource[] GetObjectSFX() { return objectSFX; }  
    public AudioSource[] GetMenuSFX() { return menuSFX; }
}
