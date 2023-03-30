using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed; // швидкість руху об'єкта
    public float rotationSpeed; // швидкість повороту об'єкта

    void Update()
    {
		float moveVertical = Input.GetAxisRaw("Vertical"); // використовуємо клавіші W та S
        int i;
        if (moveVertical < 0) i = -1;
        else i = 1;
        // Рух вперед та назад
		transform.Translate(Vector3.up * moveVertical * speed * Time.deltaTime);
        // Поворот вліво та вправо
        float rotation = Input.GetAxisRaw("Horizontal"); // використовуємо клавіші A та D
        transform.Rotate(Vector3.back * i, rotation * rotationSpeed * Time.deltaTime);
    }
}
