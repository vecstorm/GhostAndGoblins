using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirPuertas : MonoBehaviour
{
    public GameObject waypoint;
    public float platformSpeed = 2;

    audioManagerScript audioManager;// Referència a l’script que gestiona l’àudio

    // Update is called once per frame

    private void Awake()
    {
        // Busca l’objecte amb el tag "Audio" i agafa el component que gestiona els sons
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManagerScript>();
    }

    private void Start()
    {
        this.enabled = false; 
    }

    void Update()
    {
        audioManager.PlaySFX(audioManager.puertaStage);// Reprodueix l’efecte de so de la porta
        MovePlatform();//Crida al mètode que mou la plataforma cap al waypoint
    }

    //Mètode que mou l’objecte cap al waypoint a la velocitat definida
    void MovePlatform()
    {
            
        transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, platformSpeed * Time.deltaTime);

    }

}
