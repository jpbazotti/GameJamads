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
    private int chance;
    private bool shield, dampener;
    public Rigidbody2D rb;
    public Transform cannon;
    public GameObject missile,bullet;
    private SpriteRenderer shieldImage;
    void Start()
    {
        shieldImage = this.transform.GetChild(1).GetComponent<SpriteRenderer>();
        dampener = true;
        shield = true;
        moveX = 0;
    }

    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        move(moveX, moveY, dampener);

    }

    void Update()
    {
        chance = Random.Range(0, 600);
        if (chance >= 599)
        {
            shield = false;
            shieldImage.enabled = false;
            dampener = false;
            Debug.Log("dampeners off");
        }
        if (Input.GetKeyDown("space"))
        {
            int shootType = Random.Range(0, 2);
            switch (shootType)
            {
                case 0:
                    Instantiate(bullet, cannon.transform.position, Quaternion.Euler(0, 0, 0));
                    break;

                case 1:
                    Instantiate(missile, cannon.transform.position, Quaternion.Euler(0, 0, -90));
                    break;
            }
            Debug.Log(cannon.transform);
        }
        if (Input.GetKeyDown("z"))
        {
            dampener = true;
            shieldImage.enabled = true;
            shield = true;
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
        if ((x != 0 || y != 0)&&dampener)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveX * vel, accl), Mathf.MoveTowards(rb.velocity.y, moveY * vel, accl));
        }
        else if(dampener)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, desaccl), Mathf.MoveTowards(rb.velocity.y, 0, desaccl));
        }
        else
        {
            rb.AddForce(new Vector2(x*vel*2, y*vel*2));
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