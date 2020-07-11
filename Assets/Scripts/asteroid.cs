using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public Rigidbody2D rb;
    public float vel;
    void Start()
    {
        Vector3 targetLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        rb.transform.up = targetLocation - rb.transform.position;
        rb.velocity = transform.up * vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("border"))
        {
            Destroy(gameObject);
        }
    }
}
