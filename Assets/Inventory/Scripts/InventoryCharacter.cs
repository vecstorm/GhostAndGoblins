using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCharacter : MonoBehaviour
{

    [SerializeField]
    Item item;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject);
        }
    }
}
