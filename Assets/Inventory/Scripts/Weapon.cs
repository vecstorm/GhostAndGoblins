using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    [SerializeField] GameObject projectilePrefab; // Referència al prefab del projectil que dispararà l'arma.

    //Això permet a altres scripts accedir al tipus de projectil que dispara.
    public GameObject GetProjectilePrefab()
    {
        return projectilePrefab;
    }

}
