using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiControllerL : MonoBehaviour
{

    [SerializeField]
    float speed = 0;
    [SerializeField]
    float timeAnim1 = 7;
    [SerializeField]
    float timeAnim2 = 7.1f;
    //[SerializeField]
    //float timeCaminar = 0.2f;

    public Animator animator;
    Rigidbody2D rb2D;
    int dir = 1;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        Caminar(); // Hacemos que empieze caminando
        Invoke("Desaparecer", timeAnim1); // Hacemos 2 Invoke para programar los tiempos de animaciones
        Invoke("Eliminar", timeAnim2);
    }

    void Update()
    {
        rb2D.velocity = new Vector2((speed * 2) * (-1 * dir), 0f); // Le damos una velocidad constante multiplicando la dirección por -1 para que vaya al revés.
    }

    public void Desaparecer() // Hace que el zombi se quede quieto y haga la animación de meterse bajo tierra
    {
        DejarCaminar();
        animator.SetBool("desaparece", true);
    }

    public void Caminar()  // Para que empieze a moverse
    {
        speed = 1f;
    }

    public void DejarCaminar() // Para que pare de moverse
    {
        speed = 0f;
    }

    public void Eliminar()  // Para eliminar el Objeto
    {
        Destroy(gameObject);
    }

}
