using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{

    [SerializeField]
    float speed = 0;
    [SerializeField]
    float timeAnim1 = 7;
    [SerializeField]
    float timeAnim2 = 7.1f;
    
    public Animator animator;
    
    private Rigidbody2D rb2D;
    private int dir = 1;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        Caminar(); // Hacemos que empieze caminando
        Invoke("Desaparecer", timeAnim1); // Hacemos 2 Invoke para programar los tiempos de animaciones
        Invoke("Eliminar", timeAnim2);
    }

    void Update()
    {
        rb2D.velocity = new Vector2((speed * 2) * dir, 0f); // Le damos una velocidad constante
    }

    void Desaparecer() // Hace que el zombi se quede quieto y haga la animación de meterse bajo tierra
    {
        DejarCaminar();
        animator.SetBool("desaparece", true);
    }

    void Caminar() // Para que empieze a moverse
    {
        speed = 1f;
    }

    void DejarCaminar() // Para que pare de moverse
    {
        speed = 0f;
    }

    void Eliminar() // Para eliminar el Objeto
    {
        Destroy(gameObject);
    }

}
