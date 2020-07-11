using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject cam, asteroid, enemy;
    private float camHor, camVer;
    public int asteroidSpawnMaxRange, enemySpawnMaxRange;
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
        float spawnAsteroid = Random.Range(0, asteroidSpawnMaxRange);
        float spawnEnemy = Random.Range(0, enemySpawnMaxRange);

        if (spawnAsteroid >= asteroidSpawnMaxRange - 1)
        {
            Instantiate(asteroid, new Vector2(Random.Range(camPos.x, camHor), camVer + 5), Quaternion.Euler(0, 0, 0));
        } else if (spawnAsteroid <= 1)
        {
            Instantiate(asteroid, new Vector2(Random.Range(camPos.x, camHor), -(camVer + 5)), Quaternion.Euler(0, 0, 0));
        }

        if (spawnEnemy >= enemySpawnMaxRange - 1)
        {
            Instantiate(enemy, new Vector2(camHor + 5, Random.Range(-camVer, camVer)), Quaternion.Euler(0, 0, 90));
        }
    }
}
