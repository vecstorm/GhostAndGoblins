using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    float timeAnim1 = 3;
    [SerializeField]
    float timeAnim2 = 3.1f;

    public Animator animator;
    Rigidbody2D rb2D;
    int dir = 1;
    //bool salto = false; 

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        Invoke("Desaparecer", timeAnim1);
        Invoke("Eliminar", timeAnim2);
    }

    void Update()
    {
        rb2D.velocity = new Vector2(speed * dir, 0f);

        /*if (salto == true)
        {
            Eliminar();
        }*/
    }

    public void Desaparecer()
    {
        animator.SetBool("desaparece", true);

        //salto = true;
    }

    public void Eliminar()
    {
        Destroy(gameObject);
    }
}
