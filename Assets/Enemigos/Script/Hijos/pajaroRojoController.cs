using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pajaroRojoController : MonoBehaviour
{
    [SerializeField]
    float tiempoPrimeraAnimacion = 0f;
    [SerializeField]
    int distanciaPersonaje;
    [SerializeField]
    float velocidad = 2f;  // velocidad de avance

    public Animator animator;

    private Rigidbody2D rb2D;
    private GameObject player;
    private float speed = 0;
    private Vector2 posicionInicial;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // Guardamos al Player en la variable "player"

        posicionInicial = transform.position; // Guardamos la posición y la guardamos en posicionInicial
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position); // Calcula la distancia a la que se encuentra el jugador en referencia a la planta

        Vector2 direccion = (player.transform.position - transform.position).normalized; // Calculamos la direccion donde esta el jugador
        
        if (distance < distanciaPersonaje)
        {
            DejarCaminar(); // Para que empieze sin caminar
            Invoke("Transicion", tiempoPrimeraAnimacion); // Invocamos el metodo para que cuando acabe la animación haga lo que le pidamos

            float x = transform.position.x + velocidad * Time.deltaTime * direccion.x; // Calculamos el movimiento de seguimiento en el eje X
            float y = transform.position.y + velocidad * Time.deltaTime * direccion.y; // Calculamos el movimiento de seguimiento en el eje Y
            transform.position = new Vector2(x, y); // Se lo pasamos para que haga lo que le hemos pedido

            if (direccion.x > 0) // para que te siga incluso girando
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
        }
    }

    void Transicion() // Lo que hacemos en este metodo és activar la animación y llamar al método para que se empiece a mover
    {
        animator.SetBool("volar", true);
        Caminar();
    }

    void Caminar() // Seteamos la variable speed a 1 para que se mueva
    {
        speed = 1f;
    }

    void DejarCaminar() // Seteamos la variable speed a 0 para que no se mueva
    {
        speed = 0f;
    }

    public void Eliminar() // Lo creamos para que elimine el objeto
    {
        Destroy(gameObject);
    }
}
