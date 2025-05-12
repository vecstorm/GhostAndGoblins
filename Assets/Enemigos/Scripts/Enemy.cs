using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float life;
    [SerializeField]
    GameObject enemyDied;
    [SerializeField] int DiedPoints;

    public void Damage(float damage){

        life -= damage;
        if(life <= 0){
            Muerte();
        }
    }

    void Muerte(){
        PointColtroller.instance.sumarPuntos(DiedPoints);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            LiveManager.Instance.PerderVida();
        }
            
        

    }
}
