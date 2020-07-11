using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject cam, asteroid;
    public GameObject[] enemies;
    private float camHor, camVer;
    public int asteroidSpawnMaxRange, enemySpawnMaxFrames;
    private int counter;
    private Vector2 camPos;
    void Start()
    {
        camPos = cam.transform.position;
        camVer = cam.GetComponent<Camera>().orthographicSize;
        camHor = camVer * Screen.width / Screen.height;

        Debug.Log(camVer + " " + camHor);
    }

    private void FixedUpdate()
    {
        counter++;
        float spawnAsteroid = Random.Range(0, asteroidSpawnMaxRange);

        if (spawnAsteroid >= asteroidSpawnMaxRange - 1)
        {
            Instantiate(asteroid, new Vector2(Random.Range(camPos.x, camHor), camVer + 5), Quaternion.Euler(0, 0, 0));
        }
        else if (spawnAsteroid <= 1)
        {
            Instantiate(asteroid, new Vector2(Random.Range(camPos.x, camHor), -(camVer + 5)), Quaternion.Euler(0, 0, 0));
        }

        if (counter == enemySpawnMaxFrames)
        {
            counter = 0;
            Instantiate(enemies[Random.Range(0, 3)], new Vector2(camHor + 5, Random.Range(-camVer, camVer)), Quaternion.Euler(0, 0, 90));
        }
    }
}