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
        timer += Time.deltaTime; // Creem el temporitzador

        if (timer > tiempoBala)
        {
            Destroy(gameObject); // Temporitzador perquè quan passi el temps desitjat es destrueixi l'Objecte
            timer = 0; // Setegem el contador a 0
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigos"))
        {
            collision.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);

        }

        if (collision.CompareTag("Escudo"))
        {
            Destroy(gameObject);
        }
    }
}
