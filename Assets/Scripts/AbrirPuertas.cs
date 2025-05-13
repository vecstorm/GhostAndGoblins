using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    public GameObject waypoint;
    private Collider2D col;

    public float platformSpeed = 2;

    // Update is called once per frame

    private void Start()
    {
        this.enabled = false; 
        col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false; // Desactivar collider al inicio
        }
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
            
        transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, platformSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.1f)
        {
            if (col != null)
            {
                Debug.Log("La puerta se ha abierto, activando el collider...");
                col.enabled = true; // Activa el collider cuando la puerta se haya movido
            }
        }
    }

}
