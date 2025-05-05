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
    Collider2D currentFloorCollider;

    enum ESTADOS { ENSUELO, ENAIRE, MIDESCALERA, TOPESCALERA, BOTESCALERA }
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
        DetectFloor();

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
            case ESTADOS.BOTESCALERA:
                break;
            case ESTADOS.TOPESCALERA: 
                break;
        }
    }

    private void EnMovimientoSuelo(float mx)
    {
        animator.SetFloat("MovementX", Mathf.Abs(mx));
        animator.SetBool("OnStairs", false);

        // Cambia la rotación del personaje (mirar hacia la derecha o izquierda)
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
        animator.SetBool("OnStairs", true);
        rb2D.gravityScale = 0f; // Quitar gravedad mientras está en las escaleras

        // Movimiento arriba y abajo
        Vector2 climbVelocity = new Vector2(0f, my * velocidadEscalera);
        rb2D.velocity = climbVelocity;

        // Que se quede parado cuando este en las escaleras y no le este dando ni a bajar ni a subir
        if (Mathf.Approximately(my, 0f))
        {
            rb2D.velocity = Vector2.zero;
        }

        // Si el jugador está bajando y toca el suelo, vuelve al estado ENSUELO
        if (my < 0 && cercaDeEscalera && !Physics2D.Raycast(raycast.position, Vector2.down, 0.2f, floorLayer))
        {
            estado_actual = ESTADOS.ENSUELO;
            rb2D.gravityScale = gravedadInicial;
        }
    }

    private void DetectFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycast.position, Vector2.down, 0.2f, floorLayer);

        if(hit && estado_actual == ESTADOS.BOTESCALERA)
        {

        }

        if (hit && estado_actual != ESTADOS.MIDESCALERA)
        {
            estado_actual = ESTADOS.ENSUELO;
            currentFloorCollider = hit.collider;
            animator.SetBool("IsJumping", false);
        }
        else if (!hit && estado_actual != ESTADOS.MIDESCALERA)
        {
            estado_actual = ESTADOS.ENAIRE;
            animator.SetBool("IsJumping", true);
        }
        else if (hit && cercaDeEscalera && verticalMov.ReadValue<float>() < 0) // Si está tocando el suelo y la escalera y presiona el botón de bajar
        {
            // Solo permitir la bajada si se presiona hacia abajo
            if (verticalMov.ReadValue<float>() < 0)
            {
                // Mantener las colisiones activas y aplicar un pequeño retraso en la reactivación para evitar quedarse pegado
                Physics2D.IgnoreCollision(currentFloorCollider, GetComponent<Collider2D>());
                Invoke("reenableColision", 0.5f);
            }
        }
    }

    private void reenableColision()
    {
        // Rehabilitar la colisión después de un pequeño retraso
        Physics2D.IgnoreCollision(currentFloorCollider, GetComponent<Collider2D>(), false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            cercaDeEscalera = true;
            animator.SetBool("OnStairs", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            cercaDeEscalera = false;
            animator.SetBool("OnStairs", false);
            if (estado_actual == ESTADOS.MIDESCALERA)
            {
                estado_actual = ESTADOS.ENAIRE;
                rb2D.gravityScale = gravedadInicial;
            }
        }
    }


}
