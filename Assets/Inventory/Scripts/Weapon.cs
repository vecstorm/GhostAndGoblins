using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    [SerializeField] GameObject projectilePrefab;

    public GameObject GetProjectilePrefab() // aqui hace un metodo tipo GameObject para devolver el prefab del proyectil
    { 
        return projectilePrefab; 
    }

}
