using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiCasaStage2Controller : Enemy
{

    [Header("- Detección de suelo -")] // Para que se muestre un título encima de las variables
    public Transform paredCheck;
    public float radioPared = 0.2f;
    public LayerMask queEsPared;

    [Header("- Configs de zombi -")]
    [SerializeField] float speed = 0;
    [SerializeField] float timeAnim1 = 7;
    [SerializeField] float timeAnim2 = 7.1f;
    public Animator animator;

    private GameObject player;
    private Rigidbody2D rb2D;
    private bool pared;
    private bool tocaPared;
    private float tiempoUltimoGiro = 0f;
    public float cooldownGiro = 0.5f; // Tiempo en segundos antes de poder girar otra vez


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();

        Caminar(); // Hacemos que empieze caminando
    }

    void Update()
    {
        if (player != null)
        {
            pared = Physics2D.OverlapCircle(paredCheck.position, radioPared, queEsPared); // Comprueba si está tocando el suelo

            if (pared && Time.time > tiempoUltimoGiro + cooldownGiro)
            {
                // Cambia dirección
                speed *= -1f;

                // Gira el sprite en X
                transform.localScale = new Vector3(Mathf.Sign(speed) * 1.5f, 1.5f, 1.5f);

                tiempoUltimoGiro = Time.time; // Reinicia el cooldown
            }

            // Movimiento constante en la dirección actual
            rb2D.velocity = new Vector2(speed * 2f, 0f);

        }

        Debug.Log(pared);
    }

    private void OnDrawGizmosSelected() // Para que se muestre el guizmo inferior que detecta el suelo
    {
        if (paredCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(paredCheck.position, radioPared);
        }
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
