using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;  // ссылка на трансформ игрока
    public float smoothTime = 0.3f;  // время сглаживания

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
 
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = player.position;
        targetPosition.z = transform.position.z; // оставляем z-координату камеры неизменной

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }


}
