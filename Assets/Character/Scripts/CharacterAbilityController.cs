using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilityController : MonoBehaviour
{
    [SerializeField]
    GameObject weapon;
    [SerializeField] GameObject secondWeapon;
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

        Instantiate(weapon, shootSpawnPoint.position, shootSpawnPoint.rotation);  

    }

    public void ChangeWeapon(GameObject newWeapon)
    {
        weapon = newWeapon;
    }


}
