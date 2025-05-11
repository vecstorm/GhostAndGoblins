using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
private Rigidbody2D rb2D;
    private Vector2 input;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;
    [Range(0, 0.3f)] [SerializeField] private float suavizadoDeMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;
    public InputActionAsset inputActionsMapping;
    InputAction horizontal_ia, jump_ia, vertical_ia, crouch_ia;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private bool enSuelo;
    private bool salto = false;

    [Header("Animacion")]
    private Animator animator;

    [Header("Escalar")]
    [SerializeField] private float velocidadEscalar;
    private CapsuleCollider2D capsuleCollider2D;
    private float gravedadInicial;
    private bool escalando;

    [Header("Descenso")]
    [SerializeField] private LayerMask floorLayer;
    private bool canDescend = false;
    private bool ignoringFloor = false;
    private bool puedeMoverseEnHorizontal;

    enum STATES { ONSTAIRS, ONFLOOR, ONAIR, CROUCHING }
    STATES actual_state;

    void Awake()
    {
        inputActionsMapping.Enable();
        horizontal_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Horizontal");
        jump_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Jump");
        crouch_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Crouch");
        vertical_ia = inputActionsMapping.FindActionMap("Stairs").FindAction("Vertical");

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        gravedadInicial = rb2D.gravityScale;
    }

    void Update()
    {
        input.x = horizontal_ia.ReadValue<float>();
        input.y = vertical_ia.ReadValue<float>();

        salto = jump_ia.triggered && enSuelo;

        EvaluarEstado();

        switch (actual_state)
        {
            case STATES.ONFLOOR:
                EstadoSuelo();
                break;
            case STATES.ONAIR:
                EstadoAire();
                break;
            case STATES.ONSTAIRS:
                EstadoEscaleras();
                break;
            case STATES.CROUCHING:
                EstadoAgachado();
                break;
        }

        animator.SetFloat("MovementX", Mathf.Abs(rb2D.velocity.x));
    }

    void FixedUpdate()
    {
        DetectarSuelo();
        salto = false;
        animator.SetBool("IsJumping", !enSuelo);
    }

    void EvaluarEstado()
    {
        if (escalando || capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Stairs")) || (canDescend && input.y < 0))
        {
            actual_state = STATES.ONSTAIRS;
        }
        else if (!enSuelo)
        {
            actual_state = STATES.ONAIR;
        }
        else if (crouch_ia.ReadValue<float>() > 0.5f)
        {
            actual_state = STATES.CROUCHING;
        }
        else
        {
            actual_state = STATES.ONFLOOR;
        }
    }

    void EstadoSuelo()
    {
        animator.SetBool("IsJumping", false);
        movimientoHorizontal = input.x * velocidadDeMovimiento;
        Mover(movimientoHorizontal, salto);
    }

    void EstadoAire()
    {
        animator.SetBool("IsJumping", true);
        movimientoHorizontal = input.x * velocidadDeMovimiento;
        Mover(movimientoHorizontal, false);
    }

    void EstadoAgachado()
    {
        animator.SetBool("IsCrouching", true);
        movimientoHorizontal = 0f;
        Mover(0f, false);
    }

    void EstadoEscaleras()
    {
        bool enEscalera = capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Stairs"));
        bool quiereEscalar = (Mathf.Abs(input.y) > 0.05f || escalando) && (enEscalera || (canDescend && input.y < 0));

        if (quiereEscalar)
        {
            if (canDescend && input.y < 0 && !ignoringFloor)
            {
                Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Floor"), true);
                ignoringFloor = true;
            }

            Vector2 escalada = puedeMoverseEnHorizontal ?
                new Vector2(input.x * velocidadEscalar, input.y * velocidadEscalar) :
                new Vector2(0f, input.y * velocidadEscalar);

            rb2D.velocity = escalada;
            rb2D.gravityScale = 0f;
            escalando = true;
            animator.SetBool("OnStairs", true);
        }
        else
        {
            ResetFloorCollision();
            rb2D.gravityScale = gravedadInicial;
            escalando = false;
            animator.SetBool("OnStairs", false);
        }
    }

    void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha) Girar();
        else if (mover < 0 && mirandoDerecha) Girar();

        if (saltar && enSuelo)
        {
            enSuelo = false;
            animator.SetBool("IsJumping", true);
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }

    void DetectarSuelo()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayCastOrigin.position, -rayCastOrigin.up, 0.2f, queEsSuelo);
        enSuelo = hit.collider != null;
    }

    void ResetFloorCollision()
    {
        if (ignoringFloor)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Floor"), false);
            ignoringFloor = false;
        }
    }

    void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("TopStairs"))
        {
            canDescend = true;
            animator.SetBool("OnTopStairs", true);
        }
        if (colision.CompareTag("UpperStairs"))
        {
            puedeMoverseEnHorizontal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D colision)
    {
        if (colision.CompareTag("TopStairs"))
        {
            canDescend = false;
            animator.SetBool("OnTopStairs", false);
            ResetFloorCollision();
        }
        if (colision.CompareTag("UpperStairs"))
        {
            puedeMoverseEnHorizontal = false;
        }
    }

}
