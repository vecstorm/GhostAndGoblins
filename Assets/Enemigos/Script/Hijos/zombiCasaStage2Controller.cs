using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiCasaStage2Controller : Enemy
{

    [Header("- Detecci�n de suelo -")] // Para que se muestre un t�tulo encima de las variables
    public Transform paredCheck;
    public float radioPared = 0.2f;
    public LayerMask queEsPared;

    [Header("- Configs de zombi -")]
    [SerializeField] float speed = 0;
    public Animator animator;

    private GameObject player;
    private Rigidbody2D rb2D;
    private bool pared;
    private bool tocaPared;
    private float tiempoUltimoGiro = 0f;
    public float cooldownGiro = 0.5f; // Temps en segons abans de poder tornar a girar


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();

        Caminar(); // Fem que comenci caminant
    }

    void Update()
    {
        if (player != null)
        {
            pared = Physics2D.OverlapCircle(paredCheck.position, radioPared, queEsPared); // Comprueba si est� tocando el suelo

            if (pared && Time.time > tiempoUltimoGiro + cooldownGiro)
            {
                // Cambia direccio
                speed *= -1f;

                // Gira el sprite en X
                transform.localScale = new Vector3(Mathf.Sign(speed) * 1.5f, 1.5f, 1.5f);

                tiempoUltimoGiro = Time.time; // Reinicia el cooldown
            }

            // Moviment constant a la direccio actual
            rb2D.velocity = new Vector2(speed * 2f, 0f);

        }
    }

    private void OnDrawGizmosSelected() // Perquè es mostri el guisme inferior que detecta el terra
    {
        if (paredCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(paredCheck.position, radioPared);
        }
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
