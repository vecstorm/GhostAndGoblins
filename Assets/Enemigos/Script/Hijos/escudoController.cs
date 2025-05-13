using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escudoController : MonoBehaviour
{
    [SerializeField]
    float amplitud = 0.5f; // Lo alto que baja/sube
    [SerializeField]
    float frecuencia = 2f; // Lo rápido que oscila
    [SerializeField]
    float velocidad = 2f;  // velocidad de avance
    [SerializeField]
    int distanciaEscudo; // El alcance para que empiezen a moverse

    public Animator animator;

    private Rigidbody2D rb2D;
    private float speed = 0;
    private int dir = 1;
    private Vector2 posicionInicial;
    private GameObject player;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // Guardamos al Player en la variable "player"

        posicionInicial = transform.position; // Guardamos la posición y la guardamos en posicionInicial
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position); // Calcula la distancia a la que se encuentra el jugador en referencia a la planta

        if (distance < distanciaEscudo)
        {
            float x = transform.position.x + velocidad * Time.deltaTime * (-1 * dir); // Calculamos lo que tiene que hacer en el eje X
            float y = posicionInicial.y + Mathf.Sin(Time.time * frecuencia) * amplitud; // Calculamos el movimiento de oscilación en el eje Y
            transform.position = new Vector2(x, y); // Se lo pasamos para que haga lo que le hemos pedido
        }
    }

    public void Eliminar() // Lo creamos para que elimine el objeto
    {
        Destroy(gameObject);
    }
}
