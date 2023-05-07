using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool afterShot = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && afterShot)
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
}
