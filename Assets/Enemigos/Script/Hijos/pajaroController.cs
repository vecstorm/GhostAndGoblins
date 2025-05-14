using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pajaroController : MonoBehaviour
{
    [SerializeField]
    float tiempoPrimeraAnimacion = 0f;
    [SerializeField]
    float amplitud = 0.5f; // Lo alto que baja/sube
    [SerializeField]
    float frecuencia = 2f; // Lo r�pido que oscila
    [SerializeField]
    float velocidad = 2f;  // velocidad de avance
    
    public Animator animator;

    private Rigidbody2D rb2D;
    private float speed = 0;
    private int dir = 1;
    private Vector2 posicionInicial;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        posicionInicial = transform.position; // Guardamos la posici�n y la guardamos en posicionInicial

        DejarCaminar(); // Para que empieze sin caminar
        Invoke("Transicion", tiempoPrimeraAnimacion); // Invocamos el metodo para que cuando acabe la animaci�n haga lo que le pidamos
    }

    private void Update()
    {
        if (speed > 0)
        {
            float x = transform.position.x + velocidad * Time.deltaTime * dir; // Calculamos lo que tiene que hacer en el eje X
            float y = posicionInicial.y + Mathf.Sin(Time.time * frecuencia) * amplitud; // Calculamos el movimiento de oscilaci�n en el eje Y
            transform.position = new Vector2(x, y); // Se lo pasamos para que haga lo que le hemos pedido
        }
    }

    void Transicion() // Lo que hacemos en este metodo �s activar la animaci�n y llamar al m�todo para que se empiece a mover
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
