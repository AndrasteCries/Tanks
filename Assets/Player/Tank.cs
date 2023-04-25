using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tank : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Buff"))
        {
            Debug.Log(collision.gameObject.name + " pick up");
            switch (collision.gameObject.name)
            {
                case "Laser(Clone)":
                    Debug.Log("Laser pick up");
                    break;
                case "ShotGun(Clone)":
                    Debug.Log("Shotgun pick up");
                    break;
                case "SpikeBall(Clone)":
                    Debug.Log("SpikeBall pick up");
                    break;
                case "Rocket(Clone)":
                    Debug.Log("Rocket pick up");
                    break;
                default:
                    Debug.Log("Buff pick up");
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
