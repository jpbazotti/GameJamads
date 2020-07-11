using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{
    public float vel, accl, desaccl,iframes,shieldRecoverTimer;
    private float moveX, moveY, invencibility,shieldRecover;
    private int chance;
    public int hp;
    private bool shield, dampener,blinking,shieldMalfunction,shielding;
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
        blinking = false;
        shielding=false;
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
        randomEvents();
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
        if (Input.GetKeyDown("x")&&shieldMalfunction&&!shielding)
        {
            StartCoroutine(shieldManagement(true,false));
            shieldMalfunction = false;
        }
        if (Input.GetKeyDown("z"))
        {
            dampener = true;
        }

        invencibility -= Time.deltaTime;
        if (invencibility > 0 && !blinking)
        {
            StartCoroutine(blink());
        }
        shieldRecover -= Time.deltaTime;
        if (!shieldMalfunction && shieldRecover <= 0)
        {
            StartCoroutine(shieldManagement(true,false));
        }
    }

    void randomEvents()
    {
        chance = Random.Range(0, 600);
        if (chance >= 599)
        {
            dampener = false;
            Debug.Log("dampeners off");
        }
        chance = Random.Range(0, 600);
        if (chance >= 599)
        {
            StartCoroutine(shieldManagement(false, true));
            Debug.Log("shields off");

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
            if (invencibility < 0)
            {
                hp -= i;
                invencibility = iframes;
            }
           
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            StartCoroutine(shieldManagement(false, false));
        }

    }

    IEnumerator shieldManagement(bool activated,bool malfunction)
    {
        shielding = true;
        animator.SetBool("shield", activated);
        animator.SetBool("shieldMalfunction", malfunction);
        if (!malfunction)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(3.2f);
        }
        if (!malfunction)
        {
            shieldRecover = shieldRecoverTimer;
        }
        else
        {
            shieldMalfunction = true;
        }
        shield = activated;
        shielding = false;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(cannon.transform.position,5f);
    }
    IEnumerator blink()
    {
        blinking = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        blinking = false;
    }
}