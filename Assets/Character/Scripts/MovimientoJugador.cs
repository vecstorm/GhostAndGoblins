using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private Vector2 input;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;
    [Range(0, 0.3f)][SerializeField]private float suavizadoDeMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;
    public InputActionAsset inputActionsMapping;
    InputAction horizontal_ia, jump_ia, vertical_ia, crouch_ia;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private Vector3 dimensionesCaja;
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

    enum STATES {ONSTAIRS, ONFLOOR, ONAIR, ONTOPSTAIRS, ONUPPERSTAIRS, CROUCHING, SHOOT }

    STATES actual_state;



    void Awake()
    {
        GameObject jugadorExistente = GameObject.FindGameObjectWithTag("Player");

        if (jugadorExistente != null && jugadorExistente != gameObject)
        {
            Destroy(gameObject); // Previene duplicaciones del jugador
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Mantiene el jugador entre escenas
        }
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
        switch (actual_state)
        {
            case STATES.ONSTAIRS:
                break; 
            case STATES.ONFLOOR:
                break;
            case STATES.ONAIR: 
                break;
            case STATES.ONTOPSTAIRS: 
                break;
            case STATES.ONUPPERSTAIRS: 
                break;
            case STATES.CROUCHING: 
                break;
            case STATES.SHOOT:
                break;
            default:
                break;
        }
        input.x = horizontal_ia.ReadValue<float>();
        input.y = vertical_ia.ReadValue<float>();

        Crouch(); 

        if (jump_ia.triggered && enSuelo)
        {
            salto = true;
            animator.SetBool("IsJumping", true);
        }
        if (escalando && !puedeMoverseEnHorizontal)
        {
            movimientoHorizontal = 0f;
        }
        else
        {
            movimientoHorizontal = input.x * velocidadDeMovimiento;
        }

    }


    void FixedUpdate()
    {
        DetectarSuelo(); 

        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        animator.SetFloat("MovementX", Mathf.Abs(rb2D.velocity.x));

        Escalar();

        salto = false;
        animator.SetBool("IsJumping", !enSuelo);
    }

    void DetectarSuelo()
    {
        RaycastHit2D hit = Physics2D.BoxCast(rayCastOrigin.position, dimensionesCaja, 0f, Vector2.down, 0.1f, queEsSuelo);
        enSuelo = hit.collider != null;
    }





    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("TopStairs"))
        {
            canDescend = true;
            animator.SetBool("OnTopStairs", true); // <- esto activa transición a GetUpStairs
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

    void Escalar()
    {
        bool onStairs = capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Stairs"));
        bool shouldClimb = (input.y != 0 || escalando) && (onStairs || (canDescend && input.y < 0));

        if (shouldClimb)
        {
            
            if (canDescend && input.y < 0 && !ignoringFloor)
            {
                Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Floor"), true);
                ignoringFloor = true;
            }

            if(!enSuelo && !puedeMoverseEnHorizontal)
            {
                Vector2 velocidadDeSubida = new Vector2(0f, input.y * velocidadEscalar);// movimiento de subir y bajar escaleras, el movimiento horizontal se desactiva
                rb2D.velocity = velocidadDeSubida;
                rb2D.gravityScale = 0;
                escalando = true;
                animator.SetBool("OnStairs", true);
            }
            else
            {
                Vector2 velocidadDeSubida = new Vector2(input.x * velocidadEscalar, input.y * velocidadEscalar);// movimiento de subir y bajar escaleras, el movimiento horizontal se desactiva
                rb2D.velocity = velocidadDeSubida;
                rb2D.gravityScale = 0;
                escalando = false;
                animator.SetBool("OnStairs", false);
            }

            

            
            

        }
        else
        {
            ResetFloorCollision();
            rb2D.gravityScale = gravedadInicial;
            escalando = false;
            animator.SetBool("OnStairs", false);
        }

        

    }

    void ResetFloorCollision()
    {
        if (ignoringFloor)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Floor"), false);
            ignoringFloor = false;
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


    void Girar(){
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    void Crouch()
    {
        if (crouch_ia.triggered)
        {
            animator.SetBool("IsCrouching", true);
        }
        else
        {
            animator.SetBool("IsCrouching", false);
        }
    }


}
