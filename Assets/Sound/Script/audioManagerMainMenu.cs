using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerMainMenu : MonoBehaviour
{
    [Header("- Audio Source -")]
    [SerializeField] AudioSource musicSource;// Font d'àudio que reproduirà la música

    [Header("- Audio Clip -")]
    public AudioClip musicaMainMenu;// Clip de so assignat per la música del menú principal

    private void Start()
    {
        musicSource.clip = musicaMainMenu;// Assigna el clip de música a la font d’àudio
        musicSource.Play();// Reprodueix la música del menú principal
    }
}
