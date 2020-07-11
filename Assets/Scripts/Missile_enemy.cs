using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float vel, seekVel, detectionRadius;
    public LayerMask playerLayer;
    private bool targetAcquired;
    private Vector2 targetLocation;
    void Start()
    {
        rb.velocity = new Vector2(vel, 0);
        targetAcquired = false;
    }

    void Update()
    {
        if (!targetAcquired)
        {
            Detect();
        }
    }

    void FixedUpdate()
    {
        if (targetAcquired)
        {
            Seek();
        }
    }

    void Detect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, detectionRadius, playerLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[0] != null)
            {
                targetAcquired = true;
                targetLocation = colliders[i].gameObject.transform.position;
                Debug.Log(targetLocation.x + " " + targetLocation.y);
                i = colliders.Length;

            }

        }
    }
    void Seek()
    {
        Collider2D collider = Physics2D.OverlapCircle(this.transform.position, detectionRadius, playerLayer);
        targetLocation = collider.gameObject.transform.position;
        Vector2 rotateDirection = targetLocation - rb.position;
        rotateDirection.Normalize();
        float rotation = Vector3.Cross(rotateDirection, transform.up).z;
        rb.angularVelocity = rotation * seekVel;
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