using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float vel;
    public bool enemyShoot;
    void Start()
    {
        rb.velocity = transform.up * vel;
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemyShoot)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                Destroy(gameObject);

                Destroy(collision.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);

                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("border"))
        {
            Destroy(gameObject);
        }

    }
}