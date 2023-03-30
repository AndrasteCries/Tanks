using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;          // Скорость пули
    public int maxBounces = 3;         // Максимальное количество отскоков

    private int currentBounces = 0;    // Текущее количество отскоков
    private Rigidbody2D rb2d;          // Ссылка на Rigidbody2D

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else
        {
            currentBounces++;
            if (currentBounces >= maxBounces)
            {
                Destroy(gameObject);
            }
            else
            {
                // Применяем новую скорость к пуле
                rb2d.velocity = Vector2.Reflect(-collision.relativeVelocity.normalized, collision.contacts[0].normal) * speed;
            }
        }

    }
}
