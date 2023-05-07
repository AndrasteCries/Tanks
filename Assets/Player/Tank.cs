using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tank : MonoBehaviour
{
    public Transform firePoint;
    public GameObject BulletPrefab;
    public GameObject ShotGunPrefab;
    public GameObject RocketPrefab;
    public GameObject LaserPrefab;
    public GameObject SpikeBallPrefab;
    public float fireRate = 0.5f;
    public int maxBullets = 3;


    private List<GameObject> bullets = new List<GameObject>();
    private float nextFireTime;
    private GameObject currentPrefab;
    public move moveScript;
    bool afterShoot;

    private void Start()
    {
        currentPrefab = BulletPrefab;
        moveScript = GetComponent<move>();
        afterShoot = false;
    }

    void Update()
    {
        GameObject newBullet;
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime && bullets.Count < maxBullets)
        {
            if(currentPrefab == ShotGunPrefab)
            {
                for(int i = 0; i < 10; i++)
                {
                    maxBullets = 40;
                    newBullet = Instantiate(currentPrefab, firePoint.position, firePoint.rotation);
                }
            }
            else
            {
                // Создаем новую пулю и добавляем ее в список
                newBullet = Instantiate(currentPrefab, firePoint.position, firePoint.rotation);
                bullets.Add(newBullet);

                // Устанавливаем время следующего выстрела и запускаем таймер движения
                nextFireTime = Time.time + fireRate;
                if (currentPrefab == RocketPrefab)
                {
                    afterShoot = true;
                    moveScript.enabled = false;
                }
                else if (currentPrefab == SpikeBallPrefab || currentPrefab == LaserPrefab || currentPrefab == ShotGunPrefab)
                {
                    afterShoot = true;
                }
            }
        }
 
        if (currentPrefab == RocketPrefab && bullets.Count == 0 && afterShoot == true)
        {
            afterShoot = false;
            moveScript.enabled = true;
            maxBullets = 3;
            currentPrefab = BulletPrefab;
        }
        else if((currentPrefab == SpikeBallPrefab || currentPrefab == LaserPrefab || currentPrefab == ShotGunPrefab) && bullets.Count == 0 && afterShoot == true)
        {
            afterShoot = false;
            maxBullets = 3;
            currentPrefab = BulletPrefab;
        }
        // Проверяем список пуль и удаляем из него те, которые были уничтожены
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            if (bullets[i] == null)
            {
                bullets.RemoveAt(i);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Buff"))
        {
            Debug.Log(collision.gameObject.name + " pick up");
            switch (collision.gameObject.name)
            {
                case "LaserBuff(Clone)":
                case "LaserBuff":
                    Debug.Log("Laser pick up");
                    currentPrefab = LaserPrefab;
                    maxBullets = 1;
                    break;
                case "ShotGunBuff(Clone)":
                case "ShotGunBuff":
                    Debug.Log("Shotgun pick up");
                    currentPrefab = ShotGunPrefab;
                    maxBullets = 1;
                    break;
                case "SpikeBallBuff(Clone)":
                case "SpikeBallBuff":
                    Debug.Log("SpikeBall pick up");
                    currentPrefab = SpikeBallPrefab;
                    maxBullets = 1;
                    break;
                case "RocketBuff(Clone)":
                case "RocketBuff":
                    Debug.Log("Rocket pick up");
                    currentPrefab = RocketPrefab;
                    maxBullets = 1;
                    break;
                default:
                    Debug.Log("Buff pick up");
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
