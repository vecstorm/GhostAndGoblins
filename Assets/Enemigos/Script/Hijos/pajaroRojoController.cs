using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pajaroRojoController : Enemy
{
    [SerializeField]
    float tiempoPrimeraAnimacion = 0f;
    [SerializeField]
    int distanciaPersonaje;
    [SerializeField]
    float velocidad = 2f;  // velocitat d'avenç

    public Animator animator;

    private Rigidbody2D rb2D;
    private GameObject player;
    private Vector2 posicionInicial;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // Guardem al Player en la variable "player"

        posicionInicial = transform.position; // Desem la posició i la guardem en posició Inicial
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position); // Calcula la distància a què es troba el jugador en referència a la planta

            Vector2 direccion = (player.transform.position - transform.position).normalized; // Calculem la direcció on hi ha el jugador

            if (distance < distanciaPersonaje)
            {
                //DejarCaminar(); // Para que empieze sin caminar
                Invoke("Transicion", tiempoPrimeraAnimacion); // Invoquem el mètode perquè quan acabi l'animació faci el que us demanem

                float x = transform.position.x + velocidad * Time.deltaTime * direccion.x; // Calculem el moviment de seguiment a l'eix X
                float y = transform.position.y + velocidad * Time.deltaTime * direccion.y; // Calculem el moviment de seguiment a l'eix Y
                transform.position = new Vector2(x, y); // Us ho passem perquè feu el que us hem demanat

                if (direccion.x > 0) // perquè et segueixi fins i tot girant
                {
                    transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                else
                {
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                }
            }
        }
    }

    void Transicion() // El que fem en aquest metode es activar l'animacio i trucar al mrtode perqur es comenci a moure
    {
        animator.SetBool("volar", true);

    }


    public void Eliminar() // Ho creguem perquè elimini l'objecte
    {
        Destroy(gameObject);
    }
}
