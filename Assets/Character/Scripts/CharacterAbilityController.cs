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


    void Start()
    {
        MovimientoJugador c = GetComponent<MovimientoJugador>();
        shoot = c.inputActionsMapping.FindActionMap("Attack").FindAction("Shoot");

    }

    void Update()
    {
        if(shoot.triggered){
            Disparar();

        }
    }

    void Disparar(){

        Instantiate(bullet, shootSpawnPoint.position, shootSpawnPoint.rotation);  

    }

    


}
