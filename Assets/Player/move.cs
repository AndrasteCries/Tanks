using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed; // швидкість руху об'єкта
    public float rotationSpeed; // швидкість повороту об'єкта

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxisRaw("Vertical"); // використовуємо клавіші W та S
                                                           // Поворот вліво та вправо
        float rotation = Input.GetAxisRaw("Horizontal"); // використовуємо клавіші A та D
        float angle = transform.rotation.eulerAngles.z - rotation * rotationSpeed * Time.deltaTime;
        rb2d.MoveRotation(angle);

        // Получаем вектор направления из угла поворота
        Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector2.up;
        // Рух вперед та назад
        rb2d.MovePosition(rb2d.position + direction * moveVertical * speed * Time.deltaTime);
    }
}
