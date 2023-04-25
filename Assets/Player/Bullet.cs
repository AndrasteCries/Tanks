using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float minSpeed = 10f;
    public float lifetime = 2f; // время жизни в секундах

    private Vector3 lastFrameVelocity;
    private Rigidbody2D rb;
    private float timeAlive;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.velocity = transform.right * 10f;
        timeAlive = 0f; // обнуляем таймер при создании пули
    }

    void Update()
    {
        // Вычисляем значение альфа-канала, используя процент времени жизни, оставшийся для пули
        float alpha = Mathf.Clamp01((lifetime - timeAlive) / lifetime);

        // Устанавливаем новое значение альфа-канала в материале SpriteRenderer
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
        timeAlive += Time.deltaTime; // увеличиваем таймер каждый кадр
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject); // уничтожаем объект пули после окончания времени жизни
        }
        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var speed = lastFrameVelocity.magnitude;
            var direction = Vector3.Reflect(lastFrameVelocity.normalized, collision.contacts[0].normal);

            Debug.Log("Out Direction: " + direction);
            rb.velocity = direction * minSpeed;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // перезагрузка текущего уровня
        }
    }
}
