using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilityController : MonoBehaviour
{

    [SerializeField] Weapon weapon;

    [SerializeField]Transform shootSpawnPoint;
    
    InputAction shoot;

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
        if (shoot.triggered)// si la action shoot es triggered, entonces dispara i pone la variable IsShooting a true
        {
            Disparar();
            animator.SetBool("IsShooting", true);
        }
        else // sino la pone a false
        {
            animator.SetBool("IsShooting", false);
        }
    }

    void Disparar()
    {

        Instantiate(weapon.GetProjectilePrefab(), shootSpawnPoint.position, shootSpawnPoint.rotation); //instancia el prefab del proyectil
        

    }

    public void ChangeWeapon(ItemContainer newWeaponItemContainer) //crea una variable de tipo ItemContainer 
    {
        Weapon newWeapon = (Weapon)newWeaponItemContainer.GetItem(); // esta la iguala a una variable newWeapon de tipo weapon i coge la funcion de getItem para recoger el valor del item

        if (newWeapon != null)
        {
            weapon = newWeapon;
        }
    }


}
