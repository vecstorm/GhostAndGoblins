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
            GetComponent<SpriteRenderer>().sprite = item.image;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.GetComponent<CharacterAbilityController>().ChangeWeapon(this);
            Destroy(gameObject);

        }
    }

    public Item GetItem()
    {
        return item;
    }
}
