using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [Header("- Audio Source -")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("- Audio Clip -")]
    public AudioClip musicaFondo;
    public AudioClip gameOver;
    public AudioClip disparoPlayer;
    public AudioClip salto;
    public AudioClip muerteEnemigos;

    private void Start()
    {
        MusicSource.clip = musicaFondo;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
