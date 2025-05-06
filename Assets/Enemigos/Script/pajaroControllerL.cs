using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pajaroControllerL : MonoBehaviour
{
    [SerializeField]
    float tiempoPrimeraAnimacion = 0f;
    [SerializeField]
    float amplitud = 0.5f; // qué tan alto baja/sube
    [SerializeField]
    float frecuencia = 2f; // qué tan rápido oscila
    [SerializeField]
    float velocidad = 2f;  // velocidad de avance
    private Vector2 posicionInicial;

    public Animator animator;
    Rigidbody2D rb2D;
    float speed = 0;
    int dir = 1;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        posicionInicial = transform.position;

        DejarCaminar();
        Invoke("Transicion", tiempoPrimeraAnimacion);
    }

    private void Update()
    {
        if (speed > 0)
        {
            float x = transform.position.x + velocidad * Time.deltaTime * (-1 * dir);
            float y = posicionInicial.y + Mathf.Sin(Time.time * frecuencia) * amplitud;
            transform.position = new Vector2(x, y);
        }
    }

    public void Transicion ()
    {
        animator.SetBool("volar", true);
        Caminar();
    }

    public void Eliminar()
    {
        Destroy(gameObject);
    }

    public void Caminar()
    {
        speed = 1f;
    }

    public void DejarCaminar()
    {
        speed = 0f;
    }
}
