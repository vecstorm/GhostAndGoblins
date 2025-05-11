using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilityController : MonoBehaviour
{
    //[SerializeField] GameObject weapon;

    [SerializeField] Weapon weapon;

    //Asset weapon1;

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
        if (shoot.triggered)
        {
            Disparar();
            animator.SetBool("IsShooting", true);
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }
    }

    void Disparar()
    {

        Instantiate(weapon.GetProjectilePrefab(), shootSpawnPoint.position, shootSpawnPoint.rotation);

    }

    public void ChangeWeapon(ItemContainer newWeaponItemContainer)
    {
        Weapon newWeapon = (Weapon)newWeaponItemContainer.GetItem();

        if (newWeapon != null)
        {
            weapon = newWeapon;
        }
        // weapon1 = newWeapon;
    }

    public Weapon getWeapon()
    {
        return weapon;
    }

}
