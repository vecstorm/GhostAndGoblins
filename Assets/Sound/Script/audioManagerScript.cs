using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerScript : MonoBehaviour
{
    [Header("- Audio Source -")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("- Audio Clip -")]
    public AudioClip musica;
    public AudioClip gameOver;
    public AudioClip saltoPlayer;
    public AudioClip disparoPlayer;
    public AudioClip muerteEnemigo;

    private void Start()
    {
        musicSource.clip = musica;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
