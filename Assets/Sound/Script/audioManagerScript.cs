using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerScript : MonoBehaviour
{
    [Header("- Audio Source -")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;// Font d'àudio per a efectes de so (SFX)

    [Header("- Audio Clip -")]// Clips d'àudio 
    public AudioClip musica;
    public AudioClip saltoPlayer;
    public AudioClip disparoPlayer;
    public AudioClip muerteEnemigo;
    public AudioClip puertaStage;

    private void Start()
    {
        // Assigna la música principal al reproductor i la reprodueix
        musicSource.clip = musica;
        musicSource.Play();
    }

     // Mètode per reproduir un efecte de so puntual (SFX)
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
