using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class ItemContainer : MonoBehaviour
{

    [SerializeField] private Item item;


    private void OnEnable()
    {
        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item.image; //Saca el sprite Renderer del componente, o sea la imagen
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Compara si el game object collisiona con algo que tenga el tag player
        {

            collision.GetComponent<CharacterAbilityController>().ChangeWeapon(this); //cambia la arma que esta en el script CharacterAbilityController
            Destroy(gameObject); //Destruye el gameObject

        }
    }

    public Item GetItem() // hace una fucnion de tipo item que devuelve el item
    {
        return item;
    }
}
