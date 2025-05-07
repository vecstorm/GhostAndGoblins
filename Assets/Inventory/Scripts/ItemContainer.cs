using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class ItemContainer : MonoBehaviour
{
    
    [SerializeField]
    Item item;


    private void OnEnable()
    {
        if(item != null){
            GetComponent<SpriteRenderer>().sprite = item.image;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){

            collision.GetComponent<CharacterAbilityController>().ChangeWeapon(gameObject);
            //Destroy(gameObject);
            
        }
    }
}
