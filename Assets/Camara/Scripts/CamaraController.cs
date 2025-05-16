using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{

    public Transform character;
    public BoxCollider2D mapBounds; // El collider que define el mapa

    private float minX, maxX, minY, maxY;
    private float halfHeight, halfWidth;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        // Calcular la mitad del alto y ancho de la cámara
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        // Obtener límites del mapa desde el BoxCollider2D
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
