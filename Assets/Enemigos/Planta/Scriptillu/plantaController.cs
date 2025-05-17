using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantaController : Enemy
{
    [SerializeField]
    Transform bolaPos;
    [SerializeField]
    float timeIdle;
    [SerializeField]
    int distanciaBolas;
    
    public GameObject bola;
    public Animator animator;
    
    private GameObject player;
    private float time;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Guardem el Player a la variable "player"
    }

    void Update()
    {
        if(player != null) { 
            float distance = Vector2.Distance(transform.position, player.transform.position); // Calcula la distància a què es troba el jugador en referència a la planta

            if (distance < distanciaBolas)
            {
                time += Time.deltaTime; // creem un temporitzador

                if (time > 2)
                {
                    time = 0; // Posem el comptador a 0
                    Dispara();

                    animator.SetBool("dispara", true); // Li donem l'ok perquè entri a l'estat idle
                    Invoke("Idle", 0.5f); // Invoquem el mètode perquè quan acabi l'animació salti una altra vegada a idle
                }
            }

        }// Debug.Log(distance); // Per comprovar la distància a què es troba el jugador en referència a la planta
    }

    void Dispara() // Instancia una bola de las que dispara
    {
        Instantiate(bola, bolaPos.position, Quaternion.identity);
    }

    void Idle() //S'utilitza per a que el personatge entri en estat idle
    {
        animator.SetBool("dispara", false);
    }

    public void Eliminar() // Destrueix l'objecte
    {
        Destroy(gameObject);
    }
}
