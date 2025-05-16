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
            if (col.enabled == false)
            {
                Debug.Log("La puerta se ha abierto, activando el collider...");
                col.enabled = true; // Activa el collider cuando la puerta se haya movido
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Aquí puedes hacer una comprobación si el objeto que colisionó es el que deseas
        if (collision.gameObject.CompareTag("Player")) // Si el jugador colisiona
        {
            Debug.Log("El jugador ha tocado la plataforma, cambiando de escena...");

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
