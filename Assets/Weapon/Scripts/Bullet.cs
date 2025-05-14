using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    float damage;
    private float timer;
    [SerializeField]
    int tiempoBala;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        timer += Time.deltaTime; // Creamos el temporizador

        if (timer > tiempoBala)
        {
            Destroy(gameObject); // Temporizador para que cuando pase el tiempo deseado se destruya el Objeto
            timer = 0; // Seteamos el contador a 0
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigos"))
        {
            collision.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);

        }

    }



}
