using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    [SerializeField] GameObject projectilePrefab;

    public GameObject GetProjectilePrefab() 
    { 
        return projectilePrefab; 
    }

}
