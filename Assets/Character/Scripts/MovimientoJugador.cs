using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    InputAction horizontal_ia, jump_ia, vertical_ia;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
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



    void Awake()
    {
        inputActionsMapping.Enable();
        horizontal_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Horizontal");
        jump_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Jump");
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


        movimientoHorizontal = input.x * velocidadDeMovimiento;

        animator.SetFloat("MovementX", Mathf.Abs(rb2D.velocity.x));
        animator.SetBool("OnStairs", false);

        

        if(jump_ia.triggered){
            salto = true;
            animator.SetBool("IsJumping", true);
        }
    }

    void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        //Movimiento
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);

        Escalar();

        salto = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TopStairs"))
        {
            canDescend = true;
            animator.SetBool("OnTopStairs", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TopStairs"))
        {
            canDescend = false;
            animator.SetBool("OnTopStairs", false);
            ResetFloorCollision();
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

            Vector2 velocidadDeSubida = new Vector2(rb2D.velocity.x, input.y * velocidadEscalar);
            rb2D.velocity = velocidadDeSubida;
            rb2D.gravityScale = 0;
            escalando = true;
        }
        else
        {
            ResetFloorCollision();
            rb2D.gravityScale = gravedadInicial;
            escalando = false;
        }

        animator.SetBool("OnStairs", escalando);
    }

    void ResetFloorCollision()
    {
        if (ignoringFloor)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Floor"), false);
            ignoringFloor = false;
        }
    }


    void Mover(float mover, bool saltar){
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if(mover > 0 && !mirandoDerecha)
        {
            Girar();

        }
        else if(mover < 0 && mirandoDerecha)
        {
            Girar();

        }
        if(enSuelo && saltar){
            enSuelo = false;
            animator.SetBool("IsJumping", false);
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }

    void Girar(){
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }


}
