using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tumbaController : MonoBehaviour
{
    public Collider2D zombiCollider;

    void Start()
    {
        
        int tumbasLayer = LayerMask.NameToLayer("Tumbas");
        int enemigosLayer = LayerMask.NameToLayer("Enemigos");

        
        Physics2D.IgnoreLayerCollision(tumbasLayer, enemigosLayer, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
