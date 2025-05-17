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

    audioManagerScript audioManager;

    void Awake()
    {
        //iniciem el inputAction i busquem les accions que volem
        inputActionsMapping.Enable();
        horizontal_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Horizontal");
        jump_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Jump");
        crouch_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Crouch");
        vertical_ia = inputActionsMapping.FindActionMap("Stairs").FindAction("Vertical");

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        gravedadInicial = rb2D.gravityScale;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManagerScript>();
    }



    void Update()
    {
        //canvi d'estats
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
        //llegim els inputs
        input.x = horizontal_ia.ReadValue<float>();
        input.y = vertical_ia.ReadValue<float>();

        Crouch(); 

        //si es prem el salt i estem a terra
        if (jump_ia.triggered && enSuelo)
        {
            salto = true;
            animator.SetBool("IsJumping", true);
        }
        //si estem escalant i no ens podem moure horitzontalment
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
        DetectarSuelo(); //comprovem si esta tocant el terra

        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);//movem el jugador
        animator.SetFloat("MovementX", Mathf.Abs(rb2D.velocity.x));//actualitzem la animacio de corre

        Escalar();//truquem a la funcio escalar si cal

        salto = false;

        animator.SetBool("IsJumping", !enSuelo);//actualitzem la animacio de salt
    }

    //detecta si el personatge esta a sobre el terra
    void DetectarSuelo()
    {
        RaycastHit2D hit = Physics2D.BoxCast(rayCastOrigin.position, dimensionesCaja, 0f, Vector2.down, 0.1f, queEsSuelo);
        enSuelo = hit.collider != null;
    }

    //Quan entrem en un trigger
    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("TopStairs"))
        {
            canDescend = true;
            animator.SetBool("OnTopStairs", true); //activa la animacio onTopStairs
        }
        if (colision.CompareTag("UpperStairs"))
        {
            puedeMoverseEnHorizontal = true;

        }
    }

    //Quan sortirm del trigger
    private void OnTriggerExit2D(Collider2D colision)
    {
        if (colision.CompareTag("TopStairs"))
        {
            canDescend = false;
            animator.SetBool("OnTopStairs", false);//desactiva la animacio onTopStairs
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
            //si baixem escales i no hem ignorat les colisions encara
            if (canDescend && input.y < 0 && !ignoringFloor)
            {
                Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Floor"), true);
                ignoringFloor = true;
            }

            if(!enSuelo && !puedeMoverseEnHorizontal)
            {
                //nomes moviment vertical. Aixo pasa si estem en mig de les escales
                Vector2 velocidadDeSubida = new Vector2(0f, input.y * velocidadEscalar);// movimiento de subir y bajar escaleras, el movimiento horizontal se desactiva
                rb2D.velocity = velocidadDeSubida;
                rb2D.gravityScale = 0;
                escalando = true;
                animator.SetBool("OnStairs", true);
            }
            else
            {
                //activem tant el moviment vertical com horitzontal. Aixo pasa si estem tocant el terra i a sobre les escales
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

    //restaurem la colisio amb el terra si s'havia desactivat
    void ResetFloorCollision()
    {
        if (ignoringFloor)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Floor"), false);
            ignoringFloor = false;
        }
    }

    //la logica per moure el jugador
    void Mover(float mover, bool saltar)
    {
        //fa que el movimient no sigui tan brusc
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        //mira en la direccio adecuada
        if (mover > 0 && !mirandoDerecha) Girar();
        else if (mover < 0 && mirandoDerecha) Girar();

        //si salta
        if (saltar && enSuelo)
        {
            audioManager.PlaySFX(audioManager.saltoPlayer);//Activa el so de saltar

            enSuelo = false; //no esta en el terra
            animator.SetBool("IsJumping", true);
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));//afegeix força de salt
        }
    }

    //gira el personatge a l'altre costat
    void Girar(){
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    //controla si el jugador s'esta ajupint
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
