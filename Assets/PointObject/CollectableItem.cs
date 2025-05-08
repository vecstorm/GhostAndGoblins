using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item[] item;

    private void OnEnable()
    {
        InvokepointsItem();


    }

    void InvokepointsItem()
    {
        int randomIndex = UnityEngine.Random.Range(0, item.Length);
        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item[randomIndex].image;
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject);

        }
    }
}
