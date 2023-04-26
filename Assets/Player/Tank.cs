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

    GameObject currentPrefab;
    move moveScript;
    GameObject bullet;

    private void Start()
    {
        currentPrefab = BulletPrefab;
        moveScript = GetComponent<move>();
    }

    void Update()
    {
        if (bullet == null) moveScript.enabled = true;
        if (Input.GetButtonDown("Fire1"))
        {
            bullet = Instantiate(currentPrefab, firePoint.position, firePoint.rotation);
            if (currentPrefab == RocketPrefab && bullet != null)
            {
                moveScript.enabled = false;
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
                    break;
                case "ShotGunBuff(Clone)":
                case "ShotGunBuff":
                    Debug.Log("Shotgun pick up");
                    currentPrefab = ShotGunPrefab;
                    break;
                case "SpikeBallBuff(Clone)":
                case "SpikeBallBuff":
                    Debug.Log("SpikeBall pick up");
                    currentPrefab = SpikeBallPrefab;
                    break;
                case "RocketBuff(Clone)":
                case "RocketBuff":
                    Debug.Log("Rocket pick up");
                    currentPrefab = RocketPrefab;
                    break;
                default:
                    Debug.Log("Buff pick up");
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
