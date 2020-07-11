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
    public int hp;
    private bool shield, dampener;
    public Rigidbody2D rb;
    public Transform cannon;
    public GameObject missile,bullet;
    private Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetBool("shield", true);
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
            shieldHit(false,true);
            dampener = false;
            Debug.Log("dampeners off");
        }
        if (Input.GetKeyDown("space"))
        {
            int shootType = Random.Range(0, 2);
            switch (shootType)
            {
                case 0:
                    Instantiate(bullet, cannon.transform.position, Quaternion.Euler(0, 0, -90));
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
            shieldHit(true,false);
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
    public void takeDamage(int i)
    {
        if (!shield)
        {
            hp -= i;
        }
        else
        {
            shieldHit(false,false);
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void shieldHit(bool activated,bool malfunction)
    {
        shield = activated;
        animator.SetBool("shield", activated);
        animator.SetBool("shieldMalfunction", malfunction);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(cannon.transform.position,5f);
    }
}