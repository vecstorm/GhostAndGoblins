using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escudoController : Enemy
{
    [SerializeField]
    float amplitud = 0.5f; // La part alta que baixa/puja
    [SerializeField]
    float frecuencia = 2f; // Lo rapid que oscila
    [SerializeField]
    float velocidad = 2f;  // velocitat d'avenç
    [SerializeField]
    int distanciaEscudo; // L'abast perquè comencin a moure's

    public Animator animator;

    private Rigidbody2D rb2D;
    private int dir = 1;
    private Vector2 posicionInicial;
    private GameObject player;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // Guardem al Player en la variable "player"

        posicionInicial = transform.position; // Desem la posició i la guardem en "posicionInicial"
    }

    private void Update()
    {
        if (player != null) { 
            float distance = Vector2.Distance(transform.position, player.transform.position); // Calcula la distancia a la que se encuentra el jugador en referencia a la planta

            if (distance < distanciaEscudo)
            {
                float x = transform.position.x + velocidad * Time.deltaTime * (-1 * dir); // Calculem el que ha de fer a l'eix X
                float y = posicionInicial.y + Mathf.Sin(Time.time * frecuencia) * amplitud; // Calculem el moviment d'oscil·lació a l'eix Y
                transform.position = new Vector2(x, y); // Us ho passem perquè feu el que us hem demanat
            }
        }
    }

    public void Eliminar() // Ho creguem perquè elimini l'objecte
    {
        Destroy(gameObject);
    }
}
