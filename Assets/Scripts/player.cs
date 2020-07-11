using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float vel, accl, desaccl, iframes, shieldRecoverTimer;
    private float moveX, moveY, invencibility, shieldRecover;
    private int chance, shieldFixCounter;
    public int hp, shieldFix;
    private bool shield, dampener, blinking, shieldMalfunction, shieldMalfunctionAllow, shielding, shooting;
    public Rigidbody2D rb;
    public Transform cannon, cannonDown, cannonUp;
    public GameObject missile, bullet;
    private Animator animator;
    public heath_bar heath_Bar;
    public audioManager audioPlayer;
    void Start()
    {
        shieldFixCounter = shieldFix;
        animator = this.GetComponent<Animator>();
        animator.SetBool("shield", true);
        dampener = true;
        shield = true;
        shieldMalfunctionAllow = true;
        blinking = false;
        shielding = false;
        shooting = false;
        moveX = 0;
        heath_Bar.setMaxHealth(7);
        audioPlayer = FindObjectOfType<audioManager>();
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
        if (!shooting)
        {
            StartCoroutine(shoot());
        }
        if (Input.GetKeyDown("space") && shieldMalfunction && !shielding)
        {
            shieldFixCounter -= 1;
            if (shieldFixCounter <= 0)
            {
                shieldFixCounter = shieldFix;
                StartCoroutine(shieldManagement(true, false));
                shieldMalfunction = false;
                shieldMalfunctionAllow = true;
            }
        }
        if (Input.GetKeyDown("z")&&!dampener)
        {
            audioPlayer.Play("dampenerson");
            dampener = true;
        }

        invencibility -= Time.deltaTime;
        if (invencibility > 0 && !blinking)
        {
            StartCoroutine(blink());
        }
        shieldRecover -= Time.deltaTime;
        if (!shieldMalfunction && shieldRecover <= 0 && shieldMalfunctionAllow&&!shield)
        {
            StartCoroutine(shieldManagement(true, false));
        }
    }

    void randomEvents()
    {
        chance = Random.Range(0, 600);
        if (chance >= 599 && dampener)
        {
            dampener = false;
            audioPlayer.Play("dampenersoff");
            Debug.Log("dampeners off");
        }
        chance = Random.Range(0, 600);
        if (chance >= 599 && shieldMalfunctionAllow)
        {
            StartCoroutine(shieldManagement(false, true));
            Debug.Log("shields off");

        }
    }

    void move(float x, float y, bool dampener)
    {
        if ((x != 0 || y != 0) && dampener)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveX * vel, accl), Mathf.MoveTowards(rb.velocity.y, moveY * vel, accl));
        }
        else if (dampener)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, desaccl), Mathf.MoveTowards(rb.velocity.y, 0, desaccl));
        }
        else
        {
            rb.AddForce(new Vector2(x * vel * 2, y * vel * 2));
        }
    }
    public void takeDamage(int i)
    {
        if (!shield)
        {
            if (invencibility < 0)
            {
                hp -= i;
                heath_Bar.setHealth(hp);
                invencibility = iframes;
                audioPlayer.Play("hit");

            }

            if (hp <= 0)
            {
                audioPlayer.Play("death");
                Destroy(gameObject);
            }
        }
        else
        {
            StartCoroutine(shieldManagement(false, false));
        }

    }
    public void giveLife(int life)
    {
        if (hp < 7)
        {
            hp += life;
            heath_Bar.setHealth(hp);
        }
    }

    IEnumerator shieldManagement(bool activated, bool malfunction)
    {
        shielding = true;
        animator.SetBool("shield", activated);
        animator.SetBool("shieldMalfunction", malfunction);
        if (!malfunction)
        {
            shieldRecover = shieldRecoverTimer;
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            shieldMalfunctionAllow = false;
            yield return new WaitForSeconds(3.2f);
            shieldMalfunction = true;
        }
        if (activated)
        {
            audioPlayer.Play("shieldup");

        }
        else
        {
            audioPlayer.Play("shielddown");
        }
        shield = activated;
        shielding = false;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(cannon.transform.position, 5f);
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

    IEnumerator shoot()
    {
        shooting = true;
        int shootType = Random.Range(1, 101);
        
        if(shootType <= 60) {
            Instantiate(bullet, cannon.transform.position, Quaternion.Euler(0, 0, -90));
        }else if( shootType <= 90)
        {
            Instantiate(bullet, cannon.transform.position, Quaternion.Euler(0, 0, -90));
            Instantiate(bullet, cannonDown.transform.position, Quaternion.Euler(0, 0, -100));
            Instantiate(bullet, cannonUp.transform.position, Quaternion.Euler(0, 0, -80));
        }
        else
        {
            Instantiate(missile, cannon.transform.position, Quaternion.Euler(0, 0, -90));
        }

        

        yield return new WaitForSeconds(0.2f);
        shooting = false;
    }
}