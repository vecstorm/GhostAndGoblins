using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_movement : MonoBehaviour
{
    [SerializeField] Transform raycast;
    [SerializeField] LayerMask floorLayer;
    public InputActionAsset inputActionsMapping;
    

    [SerializeField] float velocidad, impulsoDeSalto, velocidadEscalera, gravedadInicial;

    Rigidbody2D rb2D;
    InputAction horizontalMov, saltarMov, verticalMov;

    BoxCollider2D boxCollider;
    Animator animator;

    enum ESTADOS { ENSUELO, ENAIRE, MIDESCALERA }
    ESTADOS estado_actual;

    bool cercaDeEscalera = false;

    private void Awake()
    {
        inputActionsMapping.Enable();
        horizontalMov = inputActionsMapping.FindActionMap("Movement").FindAction("Horizontal");
        saltarMov = inputActionsMapping.FindActionMap("Movement").FindAction("Jump");
        verticalMov = inputActionsMapping.FindActionMap("Stairs").FindAction("Vertical");

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        gravedadInicial = rb2D.gravityScale;
    }

    private void Update()
    {
        DetectarSuelo();

        float mx = horizontalMov.ReadValue<float>();
        float my = verticalMov.ReadValue<float>();

        // Si está cerca de escalera y pulsa arriba o abajo -> cambia a MIDESCALERA
        if (cercaDeEscalera && Mathf.Abs(my) > 0.1f && estado_actual != ESTADOS.MIDESCALERA)
        {
            estado_actual = ESTADOS.MIDESCALERA;
        }

        switch (estado_actual)
        {
            case ESTADOS.ENSUELO:
                EnMovimientoSuelo(mx);
                break;

            case ESTADOS.ENAIRE:
                EnMovimientoAire(mx);
                break;

            case ESTADOS.MIDESCALERA:
                EnMovimientoEscalera(mx, my);
                break;
        }
    }

    private void EnMovimientoSuelo(float mx)
    {
        animator.SetFloat("MovementX", Mathf.Abs(mx));

        //Cambia la rotacion del personaje (mirar hacia la derecha o izquierda)
        if (mx > 0)
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        else if (mx < 0)
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);

        rb2D.velocity = new Vector2(velocidad * mx, rb2D.velocity.y);

        if (saltarMov.triggered)
        {
            rb2D.AddForce(Vector2.up * impulsoDeSalto, ForceMode2D.Impulse);
        }
    }

    private void EnMovimientoAire(float mx)
    {
        // Movimiento en el aire limitado
        rb2D.velocity = new Vector2(velocidad * mx, rb2D.velocity.y);
    }

    private void EnMovimientoEscalera(float mx, float my)
    {
        rb2D.gravityScale = 0f; // quitar gravedad

        // Movimiento libre arriba/abajo y a los lados
        Vector2 climbVelocity = new Vector2(mx * velocidad, my * velocidadEscalera);
        rb2D.velocity = climbVelocity;

        if (Mathf.Approximately(mx, 0f) && Mathf.Approximately(my, 0f))
        {
            rb2D.velocity = Vector2.zero;
        }
    }

    private void DetectarSuelo()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycast.position, Vector2.down, 0.2f, floorLayer);

        if (hit && estado_actual != ESTADOS.MIDESCALERA)
        {
            estado_actual = ESTADOS.ENSUELO;
            animator.SetBool("IsJumping", false);
        }
        else if (!hit && estado_actual != ESTADOS.MIDESCALERA)
        {
            estado_actual = ESTADOS.ENAIRE;
            animator.SetBool("IsJumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            cercaDeEscalera = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            cercaDeEscalera = false;
            if (estado_actual == ESTADOS.MIDESCALERA)
            {
                estado_actual = ESTADOS.ENAIRE;
                rb2D.gravityScale = gravedadInicial;
            }
        }
    }
}
