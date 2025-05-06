using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilityController : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform shootSpawnPoint;
    InputAction shoot;
    [SerializeField]
    float coolDownShoot;

    public InputActionAsset inputActionMapping;
    private Animator animator;


    void Start()
    {
        MovimientoJugador c = GetComponent<MovimientoJugador>();
        shoot = c.inputActionsMapping.FindActionMap("Attack").FindAction("Shoot");
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if(shoot.triggered){
            Disparar();
            animator.SetBool("IsShooting", true);
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }
    }

    void Disparar(){

        Instantiate(bullet, shootSpawnPoint.position, shootSpawnPoint.rotation);  

    }

    


}
