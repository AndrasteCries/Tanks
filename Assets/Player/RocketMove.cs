using UnityEngine;

public class RocketMove : MonoBehaviour
{
    public float speed; // швидкість руху об'єкта
    public float rotationSpeed; // швидкість повороту об'єкта

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        float rotation = Input.GetAxisRaw("Horizontal") * -1; // використовуємо клавіші A та D
        transform.Rotate(Vector3.forward, rotation * rotationSpeed * Time.deltaTime);

        // зміна напрямку руху пулі відповідно до повороту
        Vector2 direction = rb2d.velocity.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += rotation * rotationSpeed * Time.deltaTime;
        Vector2 newDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rb2d.velocity = newDirection * speed;
    }
}
