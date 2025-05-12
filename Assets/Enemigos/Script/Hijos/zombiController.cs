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
    private GameObject player;
    public Animator animator;
    
    private Rigidbody2D rb2D;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();

        Caminar(); // Hacemos que empieze caminando
        Invoke("Desaparecer", timeAnim1); // Hacemos 2 Invoke para programar los tiempos de animaciones
        Invoke("Eliminar", timeAnim2);
    }

    void Update()
    {   
        Vector2 direccion = (player.transform.position - transform.position).normalized;
        rb2D.velocity = new Vector2((speed * 2) * direccion.x, 0f); // Le damos una velocidad constante
        if (direccion.x > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }

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
