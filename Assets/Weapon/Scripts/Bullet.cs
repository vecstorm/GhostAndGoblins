using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    float damage;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigos"))
        {
            collision.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("Zombi"))
        {
            collision.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);

        }
    }



}
