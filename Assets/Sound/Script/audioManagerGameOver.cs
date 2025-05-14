using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerGameOver : MonoBehaviour
{
    [Header("- Audio Source -")]
    [SerializeField] AudioSource musicSource;

    [Header("- Audio Clip -")]
    public AudioClip musicaGameOver;

    private void Start()
    {
        musicSource.clip = musicaGameOver;
        musicSource.Play();
    }
}
