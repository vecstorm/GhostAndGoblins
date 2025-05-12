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
        rb = GetComponent<Rigidbody2D>(); // Obtenemos el Rigidbody
        player = GameObject.FindGameObjectWithTag("Player"); // Obtenemos al Player

        Vector3 direccion = player.transform.position - transform.position; // Cálculo distancia a la que se encuentra el jugador para ver la dirección
        rb.velocity = new Vector2(direccion.x, direccion.y).normalized * velocidadBala; // Le damos la potencia y la dirección al Rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Creamos el temporizador

        if (timer > tiempoBala)
        {
            Destroy(gameObject); // Temporizador para que cuando pase el tiempo deseado se destruya el Objeto
            timer = 0; // Seteamos el contador a 0
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Que se destruya la bala al entrar en el collider del jugador
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            LiveManager.Instance.PerderVida();
            Destroy(gameObject);
        }
    }
}
