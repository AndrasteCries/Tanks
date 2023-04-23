using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float speed = 5f; // скорость движения
    public float maxDistance = 10f; // максимальная дистанция рейкаста
    public LayerMask obstacleMask;
    private Rigidbody2D rb2d;
    private Vector2 currentDirection;
    private BoxCollider2D playerCollider;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        currentDirection = firePoint.transform.right;
        if (Input.GetButtonDown("Fire1"))
            Shoot();
        
    }

    void Shoot()
    {
        float remainingDistance = maxDistance;
        Vector2 direction = firePoint.transform.right;
        Vector2 point = firePoint.transform.position;
        while (remainingDistance > 0f)
        {
            RaycastHit2D hit = Physics2D.Raycast(point, direction, remainingDistance, obstacleMask);
            if (hit.collider != null)
            {
                bool hitObstacle = hit.collider.CompareTag("Wall");
                if (playerCollider.OverlapPoint(point))
                {
                    // луч попал в игрока, выход из цикла
                    break;
                }
                if (hitObstacle)
                {
                    // Рикошет от препятствия
                    Debug.DrawRay(point, direction * hit.distance, Color.red, 2f);
                    direction = Vector2.Reflect(direction, hit.normal);
                    point = hit.point + direction * 0.1f; // 0.1f - небольшой сдвиг от точки столкновения
                }
                else
                {
                    Debug.DrawRay(point, direction * hit.distance, Color.yellow, 2f);
                    point = hit.point;
                }

                remainingDistance -= hit.distance;
            }
            else
            {
                Debug.DrawRay(point, direction * remainingDistance, Color.green, 2f);
                remainingDistance = 0;
            }
        }
    }




}
