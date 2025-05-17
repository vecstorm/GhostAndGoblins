using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerGameOver : MonoBehaviour
{
    [Header("- Audio Source -")]
    [SerializeField] AudioSource musicSource;// Referència a la font d'àudio que reproduirà la música

    [Header("- Audio Clip -")]
    public AudioClip musicaGameOver;// Clip de so assignat per la música de Game Over

    private void Start()
    {
        musicSource.clip = musicaGameOver;// Assigna el clip a la font d'àudio
        musicSource.Play();// Reprodueix la música de Game Over
    }
}
