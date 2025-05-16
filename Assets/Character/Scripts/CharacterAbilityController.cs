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
    int cantidadDisparos=0;

    public InputActionAsset inputActionMapping;
    private Animator animator;
    HUD currentWeapon;
    audioManagerScript audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManagerScript>();
    }

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
        audioManager.PlaySFX(audioManager.disparoPlayer);

        Instantiate(weapon.GetProjectilePrefab(), shootSpawnPoint.position, shootSpawnPoint.rotation);
        cantidadDisparos++;
    }
    public int GetCantidadDisparos()
    {
        return cantidadDisparos;
    }

    public void ChangeWeapon(ItemContainer newWeaponItemContainer)
    {
        Weapon newWeapon = (Weapon)newWeaponItemContainer.getItem();

        if (newWeapon != null)
        {
 
            weapon = newWeapon;
            
        }
        // weapon1 = newWeapon;
    }



}
