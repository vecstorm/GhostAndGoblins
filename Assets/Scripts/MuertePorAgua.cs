using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePorAgua : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprova si l’objecte que ha entrat al trigger té el tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            //accedeix al component de salut (VidesJugador000000000000)i li fa rebre 3 punts de dany (simulant la mort per caiguda a l’aigua)
            collision.gameObject.GetComponent<VidasJugador000000000000>().RecibirDano(3);
        }
    }
}
