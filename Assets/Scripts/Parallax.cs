using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, camHor, camPos;
    public GameObject cam, spriteOnLeft;
    public float parallaxEffect;

    void Start()
    {
        camPos = cam.transform.position.x;
        camHor = cam.GetComponent<Camera>().orthographicSize * Screen.width / Screen.height;
        length = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = transform.position.x;

        transform.position = new Vector3(x - parallaxEffect, transform.position.y, transform.position.z);

        if (x + length < camPos - camHor)
        {
            Debug.Log(spriteOnLeft.transform.position.x + length * 2);
            transform.position = new Vector3((spriteOnLeft.transform.position.x + length * 2) - 1, transform.position.y, transform.position.z);
        }
    }
}
