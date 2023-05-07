using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeBall : MonoBehaviour
{
    public float minSpeed = 5f;
    public float lifetime = 2f; // время жизни в секундах
    public GameObject trianglePrefab;

    private Vector3 lastFrameVelocity;
    private Rigidbody2D rb;
    private float timeAlive;
    private SpriteRenderer spriteRenderer;
    private bool afterShot = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.velocity = transform.up * 10f;
        timeAlive = 0f;
    }

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
            Boom();
            Destroy(gameObject); 
        }
        rb.velocity = rb.velocity * 0.99f;
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
        else if (collision.gameObject.CompareTag("Player") && afterShot)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // перезагрузка текущего уровня
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timeAlive > 0.2f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // перезагрузка текущего уровня
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            afterShot = true;
        }
    }

    private void Boom()
    {
        for(int i = 0; i < 30; i++)
        {
            Instantiate(trianglePrefab, transform.position, Quaternion.identity);
        }
    }
}
