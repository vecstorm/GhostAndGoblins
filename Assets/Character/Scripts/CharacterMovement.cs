using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Transform raycastOrigin;
    [SerializeField] LayerMask floorLayer;
    public InputActionAsset inputActionsMapping;
    [SerializeField] float speed, jumpImpulse, velocityStairs, initialGravity;

    Rigidbody2D rb2D;
    InputAction horizontal_ia, jump_ia, vertical_ia;

    Collider2D currentFloorCollider;

    Animator animator;
    BoxCollider2D boxCollider;

    bool onStairs;

    enum STATES
    {
        ONFLOOR, ONAIR, ONTOPSTAIRS, ONBOTSTAIRS, ONMIDSTAIRS
    }

    STATES current_state;

    private void Awake()
    {
        inputActionsMapping.Enable();
        horizontal_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Horizontal");
        jump_ia = inputActionsMapping.FindActionMap("Movement").FindAction("Jump");
        vertical_ia = inputActionsMapping.FindActionMap("Stairs").FindAction("Vertical");

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        initialGravity = rb2D.gravityScale;
    }

    void Update()
    {
        float mx = horizontal_ia.ReadValue<float>();
        float my = vertical_ia.ReadValue<float>();

        DetectFloor();

        // --- BLOQUE NUEVO: salir de la escalera si hay input horizontal en la base ---
        if (current_state == STATES.ONBOTSTAIRS && Mathf.Abs(mx) > 0.01f && Mathf.Abs(my) < 0.01f)
        {
            current_state = STATES.ONFLOOR;
            animator.SetBool("OnStairs", false);
            rb2D.gravityScale = initialGravity;
        }
        // ------------------------------------------------------------------------------

        switch (current_state)
        {
            case STATES.ONFLOOR:
                OnFloorMovement(mx);
                break;
            case STATES.ONAIR:
                OnAirMovement(mx);
                break;
            case STATES.ONTOPSTAIRS:
                OnStairsMovement(my);
                break;
            case STATES.ONBOTSTAIRS:
                OnStairsMovement(my);
                break;
            case STATES.ONMIDSTAIRS:
                OnStairsMovement(my);
                break;
        }
    }


    void OnFloorMovement(float mx)
    {
        animator.SetFloat("MovementX", Mathf.Abs(mx));
        animator.SetBool("OnStairs", false);

        rb2D.gravityScale = initialGravity;
        rb2D.velocity = new Vector2(speed * mx, rb2D.velocity.y);

        if (jump_ia.triggered && current_state == STATES.ONFLOOR)
        {
            rb2D.AddForce(new Vector2(0, 1) * jumpImpulse, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }
        // Solo ignorar colisión al bajar
        if (vertical_ia.triggered && current_state == STATES.ONFLOOR)
        {
            if (currentFloorCollider != null && boxCollider != null)
            {
                Physics2D.IgnoreCollision(currentFloorCollider, boxCollider);
                Invoke("reenableCollision", 0.5f);
            }
        }
        if (current_state == STATES.ONBOTSTAIRS)
        {
            if (horizontal_ia.triggered)
            {
                if (mx > 0)
                {
                    transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                }
                if (mx < 0)
                {
                    transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                }
            }
            else
            {
                OnStairsMovement(mx);
            }
        }
    }

    void OnAirMovement(float mx)
    {
        rb2D.gravityScale = initialGravity;
        rb2D.velocity = new Vector2(speed * mx, rb2D.velocity.y);
    }

    void OnStairsMovement(float my)
    {
        animator.SetBool("OnStairs", true);
        rb2D.gravityScale = 0f;


        rb2D.velocity = new Vector2(0f, my * velocityStairs);

        if (Mathf.Approximately(my, 0f))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
        }
    }

    bool DetectFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, Vector2.down, 0.2f, floorLayer);
        Debug.DrawLine(raycastOrigin.position, raycastOrigin.position - Vector3.up * 0.2f, Color.red);

        if (hit && hit.collider.CompareTag("Stairs"))
        {
            RaycastHit2D hitFloor = Physics2D.Raycast(raycastOrigin.position, Vector2.down, 0.3f, floorLayer);
            if (hitFloor && hitFloor.collider.CompareTag("Floor"))
            {
                current_state = STATES.ONTOPSTAIRS;
                if (currentFloorCollider != null && boxCollider != null)
                {
                    Physics2D.IgnoreCollision(currentFloorCollider, boxCollider, false);
                }
                currentFloorCollider = hitFloor.collider;
                return true;
            }
            else
            {
                current_state = STATES.ONBOTSTAIRS;
                // Si quieres, puedes guardar aquí el suelo de abajo si lo necesitas
                return true;
            }
        }

        else if (hit)
        {
            current_state = STATES.ONFLOOR;
            // Al pisar cualquier suelo, asegúrate de reactivar la colisión
            if (currentFloorCollider != null && boxCollider != null)
            {
                Physics2D.IgnoreCollision(currentFloorCollider, boxCollider, false);
            }
            currentFloorCollider = hit.collider;
            animator.SetBool("IsJumping", false);
        }
        else
        {
            current_state = STATES.ONAIR;
            animator.SetBool("IsJumping", true);
        }

        return false;
    }

    void reenableCollision()
    {
        if (currentFloorCollider != null && boxCollider != null)
        {
            Physics2D.IgnoreCollision(currentFloorCollider, boxCollider, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            onStairs = true;
            animator.SetBool("OnStairs", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            onStairs = false;
            animator.SetBool("OnStairs", false);
        }
    }

    
}
