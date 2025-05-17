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
    GameObject[] collectableItems; // Array de possibles objectes a deixar anar
    [SerializeField]
    GameObject[] collectableWapon; // Array de posibles armas a deixar anar
    [SerializeField] 
    float dropChanceItem = 0.5f; // Probabilitat de deixar anar objecte
    [SerializeField]
    float dropChanceWeapon = 0.2f;
    int enemigosDerrotados;


    audioManagerScript audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManagerScript>();
        enemigosDerrotados = 0;
    }

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
        else
        {
            
        }
        PointColtroller.instance.sumarPuntos(DiedPoints);
        DropItem(); // Intentem deixar anar objecte
        DropWeapon();
        PointColtroller.instance.sumarEnemigosMuertos();
        Destroy(gameObject);

        audioManager.PlaySFX(audioManager.muerteEnemigo);
    }

    public int GetEnemigosDerrotados()
    {
        return enemigosDerrotados;
    }
    private void DropItem()
    {
        if (collectableItems.Length > 0 && Random.value < dropChanceItem) // Probabilitat de deixar anar objecte
        {
            int randomIndex = Random.Range(0, collectableItems.Length);
            Instantiate(collectableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
    private void DropWeapon()
    {
        if (collectableWapon.Length > 0 && Random.value < dropChanceWeapon) // Probabilitat de deixar anar objecte
        {
            int randomIndex = Random.Range(0, collectableWapon.Length);
            Instantiate(collectableWapon[randomIndex], transform.position, Quaternion.identity);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<VidasJugador000000000000>().RecibirDano(1);
        }
    }


}
