using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : Enemy
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

        Caminar(); // Fem que comenci caminant
        Invoke("Desaparecer", timeAnim1); // Fem 2 Invoke per programar els temps d'animacions
        Invoke("Eliminar", timeAnim2);
    }

    void Update()
    {
        if (player != null)
        {
                Vector2 direccion = (player.transform.position - transform.position).normalized;
        
            rb2D.velocity = new Vector2((speed * 2) * direccion.x, 0f); // Li donem una velocitat constant
            if (direccion.x > 0)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
        }
    }

    void Desaparecer() // Fa que el zombi es quedi quiet i faci l'animació de ficar-se sota terra
    {
        DejarCaminar();
        animator.SetBool("desaparece", true);
    }

    void Caminar() // Perquè comenci a moure's
    {
        speed = 1f;
    }

    void DejarCaminar() // Perquè pari de moure's
    {
        speed = 0f;
    }

    void Eliminar() // Per eliminar l'Objecte
    {
        Destroy(gameObject);
    }

}
