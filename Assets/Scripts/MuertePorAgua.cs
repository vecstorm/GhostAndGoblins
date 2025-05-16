using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePorAgua : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<VidasJugador000000000000>().RecibirDano(3);
        }
    }
}
