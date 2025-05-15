using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerMainMenu : MonoBehaviour
{
    [Header("- Audio Source -")]
    [SerializeField] AudioSource musicSource;

    [Header("- Audio Clip -")]
    public AudioClip musicaMainMenu;

    private void Start()
    {
        musicSource.clip = musicaMainMenu;
        musicSource.Play();
    }
}
