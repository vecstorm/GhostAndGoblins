using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : Enemy
{
    public GameObject puerta1;
    public GameObject puerta2;
    private Rigidbody2D rb2D;
    private GameObject player;
    private bool suelo = false;
    private bool haSaltado = false;      // Per saber si ha saltat
    private bool estuvoEnElAire = false; // per saber si ha estat en el aire

    [Header("Configuraci�n de salto")] // Perquè es mostri un títol sobre les variables
    public float rangoVision = 10f;
    public float fuerzaSaltoX = 2f;
    public float fuerzaSaltoY = 8f;
    public float cooldownSalto = 4f; // Temps entre salts en segons
    private float tiempoUltimoSalto = -Mathf.Infinity;

    [Header("Detecci�n de suelo")] // Perquè es mostri un títol sobre les variables
    public Transform sueloCheck;
    public float radioSuelo = 0.2f;
    public LayerMask queEsSuelo;
    public Animator animator;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Asigna el Rigidbody2D del enemic
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            suelo = Physics2D.OverlapCircle(sueloCheck.position, radioSuelo, queEsSuelo); // Comprova si esta tocant el terra

            float distancia = Vector2.Distance(transform.position, player.transform.position);  // Calcula la distància entre l'enemic i el jugador

            if (distancia < rangoVision && suelo && Time.time > tiempoUltimoSalto + cooldownSalto) // Mirem si tot està correcte
            {
                SaltarHaciaJugador(); // Executem el salt cap al jugador
            }

            // Si ha saltat i ja NO esta a terra, marquem que va estar a l'aire
            if (haSaltado && !suelo)
            {
                estuvoEnElAire = true;
            }

            // Si ha saltat, va estar en el aire, y ara ha tornat al terra -> reset animacions
            if (haSaltado && estuvoEnElAire && suelo)
            {
                animator.SetBool("jumpL", false);
                animator.SetBool("jumpR", false);

                haSaltado = false;        // Resetegem tot pel següent salt
                estuvoEnElAire = false;
            }
        }
    }

    private void SaltarHaciaJugador()
    {
        Vector2 direccion = (player.transform.position - transform.position).normalized; // Calculem la direccio en la que esta el jugador
        Vector2 fuerza = new Vector2(direccion.x * fuerzaSaltoX, fuerzaSaltoY); // Calculem la força desitjada

        if (direccion.x < 0)
        {
            animator.SetBool("jumpL", true);
        } else if (direccion.x > 0)
        {
            animator.SetBool("jumpR", true);
        }

        haSaltado = true;        // Marquem que ha saltar
        estuvoEnElAire = false;  // Reiniciem aquesta variable

        rb2D.AddForce(fuerza, ForceMode2D.Impulse); // Li donem un impuls amb els calculs d'abans

        tiempoUltimoSalto = Time.time; // Reinicia el cooldown
    }

    private void OnDrawGizmosSelected() // Perquè es mostri el guisme inferior que detecta el terra
    {
        if (sueloCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sueloCheck.position, radioSuelo);
        }
    }

    public void Morir()
    {

        if (puerta1 != null && puerta2 != null)
        {
            puerta1.GetComponent<AbrirPuertaDerecha>().enabled = true; // Activa el movement de la porta 1
            puerta2.GetComponent<AbrirPuertas>().enabled = true; // Activa el moviment de la porta 2
        }

    }
    

}
