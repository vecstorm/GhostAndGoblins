using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float life;
    [SerializeField]
    GameObject enemyDied;

    public void Damage(float damage){

        life -= damage;
        if(life <= 0) // si la vida es menor o igual a 0, el enemigo muere
        {
            Death();
        }
    }

    void Death(){
        Destroy(gameObject); // destruye el game object
    }

    
}
