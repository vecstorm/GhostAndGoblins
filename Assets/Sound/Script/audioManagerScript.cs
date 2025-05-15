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
    public AudioClip saltoPlayer;
    public AudioClip disparoPlayer;
    public AudioClip muerteEnemigo;
    public AudioClip puertaStage;

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
