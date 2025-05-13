using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false; // Desactiva el collider hasta que la puerta se abra
    }

    public void ActivarCollider()
    {
        Debug.Log("La puerta se ha abierto, activando el collider...");
        col.enabled = true; // Activa el collider cuando la puerta se haya movido
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && col.enabled) // Verifica si el jugador entra en el área de la puerta
        {
            SceneManager.LoadScene(2); // Carga la nueva escena
        }
    }
}
