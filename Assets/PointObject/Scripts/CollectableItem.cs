using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item[] item;
    int randomIndex;
    int points;

    private void OnEnable()
    {
        InvokepointsItem();


    }

    void InvokepointsItem()
    {
        randomIndex = UnityEngine.Random.Range(0, item.Length);
        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = item[randomIndex].image;
            PointObjects pointObject = item[randomIndex] as PointObjects;
            if (pointObject != null)
            {
                points = pointObject.getPoints();
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PointControler.Instance.sumarPuntos(points);
            Destroy(gameObject);

        }
    }


}
