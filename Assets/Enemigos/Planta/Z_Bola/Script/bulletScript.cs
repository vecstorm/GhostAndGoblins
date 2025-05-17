using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField]
    float velocidadBala;
    [SerializeField]
    int tiempoBala;
    
    private GameObject player;
    private Rigidbody2D rb;

    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtenim el Rigidbody
        player = GameObject.FindGameObjectWithTag("Player"); // Obtenim al Player

        Vector3 direccion = player.transform.position - transform.position; //Calcul distancia a la que es troba el jugador per veure la direccio
        rb.velocity = new Vector2(direccion.x, direccion.y).normalized * velocidadBala; // Li donem la potència i la direccio al Rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Creem el temporitzador

        if (timer > tiempoBala)
        {
            Destroy(gameObject); // Temporitzador perquè quan passi el temps desitjat es destrueixi l'Objecte
            timer = 0; // Posem el comptador a 0
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Que es destrueixi la bala en entrar al collider del jugador
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<VidasJugador000000000000>().RecibirDano(1);
            Destroy(gameObject);
        }
    }
}
