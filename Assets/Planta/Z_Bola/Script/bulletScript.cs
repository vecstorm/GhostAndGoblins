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
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direccion = player.transform.position - transform.position; // Cálculo distancia a la que se encuentra el jugador para ver la dirección
        rb.velocity = new Vector2(direccion.x, direccion.y).normalized * velocidadBala;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > tiempoBala)
        {
            Destroy(gameObject);
            timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
