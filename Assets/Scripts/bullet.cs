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
        rb.velocity = new Vector2(vel, 0);
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

                collision.GetComponent<player>().takeDamage(1);
            }
        }
        if (collision.gameObject.CompareTag("border_despawn"))
        {
            Destroy(gameObject);
        }

    }
}