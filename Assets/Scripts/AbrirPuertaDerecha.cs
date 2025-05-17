using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirPuertaDerecha : MonoBehaviour
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
            col.enabled = false; // Desactiva el collider al principi
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
            if (col.enabled == false)
            {
                Debug.Log("La puerta se ha abierto, activando el collider...");
                col.enabled = true; // Activa el collider quan la porta s'hagi mogut
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Aqui pots fer una comprovacio si l'objecte que colisiona es el que vols
        if (collision.gameObject.CompareTag("Player")) // Si el jugador colisiona
        {
            Debug.Log("El jugador ha tocado la plataforma, cambiando de escena...");

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
