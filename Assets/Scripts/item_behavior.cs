using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_behavior : MonoBehaviour
{
    public float accl, acclToPlayer, delay;
    private Vector3 player;
    public Rigidbody2D rb;
    private bool shooting;
    private float positionY;
    public float Xvel;
    void Start()
    {
        rb.velocity = new Vector2(-(Xvel), 0);
        positionY = Random.Range(-18f, 18f);
    }



    // Update is called once per frame
    void Update()
    {
        rb.transform.position = new Vector2(rb.position.x, Mathf.MoveTowards(rb.position.y, positionY, accl));

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            collision.GetComponent<player>().giveLife(1);
        }
        if (collision.gameObject.CompareTag("border_despawn"))
        {
            Destroy(gameObject);
        }
    }


}