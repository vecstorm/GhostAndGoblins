using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControllerCasa : Enemy
{
    [SerializeField] float speed = 2f;
    public GameObject[] waypoints;

    public Animator animator;
    private Rigidbody2D rb2D;

    private Transform objetivoActual;
    private int waypointsIndex = 0;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (waypoints.Length > 0)
        objetivoActual = waypoints[waypointsIndex].transform;

    }

    void Update()
    {
        //if (objetivoActual == null) return;

        Vector2 direccion = (objetivoActual.position - transform.position).normalized;

        // Mover al zombie
        rb2D.velocity = new Vector2(direccion.x * speed, rb2D.velocity.y);

        // Cambiar escala para mirar hacia la dirección
        if (direccion.x > 0)
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        else
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);

        // Cambiar de objetivo si llegó al actual
        if (Vector2.Distance(transform.position, waypoints[waypointsIndex].transform.position) < 0.1f)
        {
            waypointsIndex++;

            if (waypointsIndex >= waypoints.Length)
            {
                waypointsIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointsIndex].transform.position, speed * Time.deltaTime);

        // Controlar animaciones si tienes alguna
        animator.SetFloat("speed", Mathf.Abs(rb2D.velocity.x));
    }
}
