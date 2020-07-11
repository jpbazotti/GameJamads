﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_behavior_2 : MonoBehaviour
{
    public float accl, acclToPlayer, delay;
    private Vector3 player;
    public Rigidbody2D rb;
    public GameObject missile;
    public Transform castPoint, castPoint2, castPoint3;
    private bool shooting;
    void Start()
    {
        rb.velocity = new Vector2(-4, 0);
    }



    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;

        rb.transform.position = new Vector2(rb.position.x, Mathf.MoveTowards(rb.position.y, player.y, accl));


        //ver player
        Vector2 start = castPoint.transform.position;

        RaycastHit2D sighttest = Physics2D.Raycast(start, castPoint.right);

        if (sighttest.collider.tag == "Player" && shooting == false)
        {
            StartCoroutine(bang(sighttest));

        }
    }

    IEnumerator bang(RaycastHit2D sighttest)
    {
        shooting = true;
        Debug.Log(sighttest.collider.tag);
        Instantiate(missile, castPoint.transform.position, Quaternion.Euler(0, 0, -90));
        Instantiate(missile, castPoint2.transform.position, Quaternion.Euler(0, 0, -120));
        Instantiate(missile, castPoint3.transform.position, Quaternion.Euler(0, 0, -45));
        Debug.Log(castPoint.transform);
        yield return new WaitForSeconds(0.5f);
        shooting = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            collision.GetComponent<player>().takeDamage(1);
        }
        if (collision.gameObject.CompareTag("border_despawn"))
        {
            Destroy(gameObject);
        }
    }
}
