using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float JUMP_FORCE = 250;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * JUMP_FORCE);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //colliding with buildings
        if (collision.gameObject.CompareTag("Building"))
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
