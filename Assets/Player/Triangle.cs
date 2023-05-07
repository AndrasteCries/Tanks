using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Triangle : MonoBehaviour
{
    public float minSpeed = 10f;
    public float lifetime = 3f; // время жизни в секундах

    private Vector3 lastFrameVelocity;
    private Rigidbody2D rb;
    private float timeAlive;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        float angle = Random.Range(0f, 360f);
        Vector3 randomDirection = Quaternion.Euler(0f, 0f, angle) * transform.up;
        rb.velocity = randomDirection * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = Mathf.Clamp01((lifetime - timeAlive) / lifetime);

        // Устанавливаем новое значение альфа-канала в материале SpriteRenderer
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
        timeAlive += Time.deltaTime; // увеличиваем таймер каждый кадр
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
        rb.velocity = rb.velocity * 0.98f;
        lastFrameVelocity = rb.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var speed = lastFrameVelocity.magnitude;
            var direction = Vector3.Reflect(lastFrameVelocity.normalized, collision.contacts[0].normal);

            Debug.Log("Out Direction: " + direction);
            rb.velocity = direction * speed;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // перезагрузка текущего уровня
        }
    }
}
