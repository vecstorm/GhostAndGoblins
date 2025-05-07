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
        if(life <= 0){
            Muerte();
        }
    }

    void Muerte(){
        Instantiate(enemyDied, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
}
