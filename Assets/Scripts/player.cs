using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{
    public float vel, accl, desaccl;
    private float moveX, moveY;
    public Rigidbody2D rb;
    public Transform cannon;
    public GameObject missile;
    void Start()
    {
        moveX = 0;
    }

    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        move(moveX, moveY, true);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(missile, cannon.transform.position, Quaternion.Euler(0, 0, -90));
            Debug.Log(cannon.transform);
        }

        if (rb.velocity.y > 0)
        {
          
        }
        else if (rb.velocity.y < 0)
        {
            
        }
        else
        {
           
        }
    }

    void move(float x, float y, bool dampener)
    {
        if (x != 0 || y != 0)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveX * vel, accl), Mathf.MoveTowards(rb.velocity.y, moveY * vel, accl));
        }
        else if(dampener)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, desaccl), Mathf.MoveTowards(rb.velocity.y, 0, desaccl));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);

            Destroy(collision.gameObject);

            SceneManager.LoadScene("Menu");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(cannon.transform.position,5f);
    }
}