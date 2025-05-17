using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pajaroControllerL : Enemy
{
    [SerializeField]
    float tiempoPrimeraAnimacion = 0f;
    [SerializeField]
    float amplitud = 0.5f; // que tan alt baixa/puja
    [SerializeField]
    float frecuencia = 2f; // que tan ràpid oscil·la
    [SerializeField]
    float velocidad = 2f;  // velocitat d'avenç
    
    public Animator animator;

    private Rigidbody2D rb2D;
    private float speed = 0;
    private int dir = 1;
    private Vector2 posicionInicial;


    void Start()
    {
        transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        rb2D = GetComponent<Rigidbody2D>();

        posicionInicial = transform.position; // Desem la posició i la guardem en posició Inicial

        DejarCaminar(); // Perquè comenci sense caminar
        Invoke("Transicion", tiempoPrimeraAnimacion); // Invoquem el mètode perquè quan acabi l'animació faci el que us demanem
    }

    private void Update()
    {

        if (speed > 0)
        {
            float x = transform.position.x + velocidad * Time.deltaTime * (-1 * dir); // Calculem el que ha de fer a l'eix X i multipliquem l'adreça per -1 perquè vagi al revés
            float y = posicionInicial.y + Mathf.Sin(Time.time * frecuencia) * amplitud; // Calculem el moviment d'oscilacio a l'eix Y
            transform.position = new Vector2(x, y); // ho passem perquè fegi el que hem demanat
        }
    }

    public void Transicion() // El que fem en aquest metode es activar l'animacio i trucar al metode perqu es comenci a moure
    {
        animator.SetBool("volar", true);
        Caminar();
    }

    public void Caminar() // Setegem la variable speed a 1 perquè es mogui
    {
        speed = 1f;
    }

    public void DejarCaminar() // Setegem la variable speed a 0 perquè no es mogui
    {
        speed = 0f;
    }

    public void Eliminar() // Ho creguem perquè elimini l'objecte
    {
        Destroy(gameObject);
    }
}
