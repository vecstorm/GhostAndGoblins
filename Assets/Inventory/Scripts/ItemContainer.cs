
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
            GetComponent<SpriteRenderer>().sprite = item.image;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Crida el mètode "ChangeWeapon" del jugador i li passa aquest contenidor (amb l'item a dins)
            collision.GetComponent<CharacterAbilityController>().ChangeWeapon(this);
            Destroy(gameObject);

        }
    }

    //Mètode que retorna l’item que conté aquest contenidor
    public Item getItem()
    {
        return item;
    }

}
