using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : Enemy
{
    private Rigidbody2D rb2D;
    private GameObject player;
    private bool suelo = false;
    private bool haSaltado = false;      // Para saber si ha saltado
    private bool estuvoEnElAire = false; // Para saber si ha estado en el aire

    [Header("Configuración de salto")] // Para que se muestre un título encima de las variables
    public float rangoVision = 10f;
    public float fuerzaSaltoX = 2f;
    public float fuerzaSaltoY = 8f;
    public float cooldownSalto = 4f; // Tiempo entre saltos en segundos
    private float tiempoUltimoSalto = -Mathf.Infinity;

    [Header("Detección de suelo")] // Para que se muestre un título encima de las variables
    public Transform sueloCheck;
    public float radioSuelo = 0.2f;
    public LayerMask queEsSuelo;
    public Animator animator;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Asigna el Rigidbody2D del enemigo
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        suelo = Physics2D.OverlapCircle(sueloCheck.position, radioSuelo, queEsSuelo); // Comprueba si está tocando el suelo

        float distancia = Vector2.Distance(transform.position, player.transform.position);  // Calcula la distancia entre el enemigo y el jugador

        if (distancia < rangoVision && suelo && Time.time > tiempoUltimoSalto + cooldownSalto) // Miramos si todo esta correcto
        {
            SaltarHaciaJugador(); // Ejecutamos el salto hacia eel jugador
        }

        // Si ha saltado y ya NO está en el suelo, marcamos que estuvo en el aire
        if (haSaltado && !suelo)
        {
            estuvoEnElAire = true;
        }

        // Si ha saltado, estuvo en el aire, y ahora volvió al suelo -> reset animaciones
        if (haSaltado && estuvoEnElAire && suelo)
        {
            animator.SetBool("jumpL", false);
            animator.SetBool("jumpR", false);

            haSaltado = false;        // Reseteamos todo para el siguiente salto
            estuvoEnElAire = false;
        }
    }

    private void SaltarHaciaJugador()
    {
        Vector2 direccion = (player.transform.position - transform.position).normalized; // Calculamos la dirección en la que esta el jugador
        Vector2 fuerza = new Vector2(direccion.x * fuerzaSaltoX, fuerzaSaltoY); // Calculamos la fuerza deseada

        if (direccion.x < 0)
        {
            animator.SetBool("jumpL", true);
        } else if (direccion.x > 0)
        {
            animator.SetBool("jumpR", true);
        }

        haSaltado = true;        // Marcamos que ha saltado
        estuvoEnElAire = false;  // Reiniciamos esta variable

        rb2D.AddForce(fuerza, ForceMode2D.Impulse); // Le damos el impulso con los calculos anteriores

        tiempoUltimoSalto = Time.time; // Reinicia el cooldown
    }

    private void OnDrawGizmosSelected() // Para que se muestre el guizmo inferior que detecta el suelo
    {
        if (sueloCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sueloCheck.position, radioSuelo);
        }
    }
}
