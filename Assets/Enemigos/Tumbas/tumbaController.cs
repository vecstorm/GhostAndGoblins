using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tumbaController : MonoBehaviour
{
    public Collider2D zombiCollider;

    void Start()
    {
        // Asegurarse de obtener el CompositeCollider2D
        CompositeCollider2D tumbasCollider = GetComponent<CompositeCollider2D>();

        // Ignorar colisión si ambos colliders existen
        if (tumbasCollider != null && zombiCollider != null)
        {
            Physics2D.IgnoreCollision(tumbasCollider, zombiCollider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
