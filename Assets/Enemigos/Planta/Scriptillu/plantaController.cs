using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantaController : MonoBehaviour
{
    [SerializeField]
    Transform bolaPos;
    [SerializeField]
    float timeIdle;
    [SerializeField]
    int distanciaBolas;
    
    public GameObject bola;
    public Animator animator;
    
    private GameObject player;
    private float time;

    // A�adir script de da�o

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Guardamos al Player en la variable "player"
    }

    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position); // Calcula la distancia a la que se encuentra el jugador en referencia a la planta

        if (distance < distanciaBolas)
        {
            time += Time.deltaTime; // creamos un temporizador

            if (time > 2)
            {
                time = 0; // Seteamos el contador a 0
                Dispara();

                animator.SetBool("dispara", true); // Le damos el ok para que entre al estado idle
                Invoke("Idle", 0.5f); // Invocamos el metodo para que cuando acabe la animaci�n salte otra vez a idle
            }
        }

        // Debug.Log(distance); // Para comprobar la distancia a la que se encuentra el jugador en referencia a la planta
    }

    void Dispara() // Instancia una bola de las que dispara
    {
        Instantiate(bola, bolaPos.position, Quaternion.identity);
    }

    void Idle() // Se usa para que el personaje entre en el estado idle
    {
        animator.SetBool("dispara", false);
    }

    public void Eliminar() // Destruye el objeto
    {
        Destroy(gameObject);
    }
}
