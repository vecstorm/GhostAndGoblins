using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{

    public Transform character;
    public BoxCollider2D mapBounds; // El collider que defineix el mapa

    private float minX, maxX, minY, maxY;
    private float halfHeight, halfWidth;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        // Calcular la meitat de la altura i la amplada de la camara
        halfHeight = cam.orthographicSize; 
        halfWidth = halfHeight * cam.aspect;

        //Obtenir limits del mapa des del BoxCollider2D

        Bounds bounds = mapBounds.bounds;

        minX = bounds.min.x + halfWidth;
        maxX = bounds.max.x - halfWidth;

    }

    void LateUpdate()
    {
        if (character == null) return;

        // Seguir al jugador
        float clampedX = Mathf.Clamp(character.position.x, minX, maxX);


        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }


}
