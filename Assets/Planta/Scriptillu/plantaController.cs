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

    // Añadir script de daño

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanciaBolas)
        {
            time += Time.deltaTime;

            if (time > 2)
            {
                time = 0;
                dispara();

                animator.SetBool("dispara", true);
                Invoke("Idle", 0.5f);
            }
        }

        Debug.Log(distance);
    }

    void dispara()
    {
        Instantiate(bola, bolaPos.position, Quaternion.identity);
    }

    public void Eliminar()
    {
        Destroy(gameObject);
    }

    public void Idle()
    {
        animator.SetBool("dispara", false);
    }
}
