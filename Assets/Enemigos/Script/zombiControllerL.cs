using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiControllerL : MonoBehaviour
{

    [SerializeField]
    float speed = 0;
    [SerializeField]
    float timeAnim1 = 7;
    [SerializeField]
    float timeAnim2 = 7.1f;
    //[SerializeField]
    //float timeCaminar = 0.2f;

    public Animator animator;
    Rigidbody2D rb2D;
    int dir = 1;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        Caminar();
        Invoke("Desaparecer", timeAnim1);
        Invoke("Eliminar", timeAnim2);
    }

    void Update()
    {
        rb2D.velocity = new Vector2((speed * 2) * (-1 * dir), 0f);
    }

    public void Desaparecer()
    {
        DejarCaminar();
        animator.SetBool("desaparece", true);
    }

    public void Eliminar()
    {
        Destroy(gameObject);
    }

    public void Caminar()
    {
        speed = 1f;
    }

    public void DejarCaminar()
    {
        speed = 0f;
    }

}
