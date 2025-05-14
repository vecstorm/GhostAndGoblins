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
    [SerializeField] 
    GameObject[] collectableItems; // Array de posibles objetos a soltar
    [SerializeField]
    GameObject[] collectableWapon; // Array de posibles armas a soltar
    [SerializeField] 
    float dropChanceItem = 0.5f; // Probabilidad de soltar un objeto (50%)
    [SerializeField]
    float dropChanceWeapon = 0.2f;

    public void Damage(float damage){

        life -= damage;
        if(life <= 0){
            Muerte();
        }
    }

    void Muerte(){

        if (GetComponent<bossController>() != null)
        {
            GetComponent<bossController>().Morir();
        }
        PointColtroller.instance.sumarPuntos(DiedPoints);
        DropItem(); // Intentamos soltar un objeto
        DropWeapon();
        Destroy(gameObject);
    }
    private void DropItem()
    {
        if (collectableItems.Length > 0 && Random.value < dropChanceItem) // Probabilidad de soltar objeto
        {
            int randomIndex = Random.Range(0, collectableItems.Length);
            Instantiate(collectableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
    private void DropWeapon()
    {
        if (collectableWapon.Length > 0 && Random.value < dropChanceWeapon) // Probabilidad de soltar objeto
        {
            int randomIndex = Random.Range(0, collectableWapon.Length);
            Instantiate(collectableWapon[randomIndex], transform.position, Quaternion.identity);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            LiveManager.Instance.PerderVida();
        }
    }
}
