using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class missile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float vel,seekVel,detectionRadius;
    public LayerMask enemyLayer;
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position,detectionRadius ,enemyLayer );
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[0]!=null)
            {
                targetAcquired=true;
                targetLocation = colliders[i].gameObject.transform.position;
                Debug.Log(targetLocation.x+" "+targetLocation.y);
                i = colliders.Length;

            }

        }
    }
    void Seek()
    {
        Vector2 rotateDirection = targetLocation - rb.position;
        rotateDirection.Normalize();
        float rotation = Vector3.Cross(rotateDirection, transform.up).z;
        rb.angularVelocity = -rotation * seekVel;
        rb.velocity = transform.up * vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("border_despawn"))
        {
            Destroy(gameObject);
        }

    }
}