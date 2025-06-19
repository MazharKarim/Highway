using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCarSpawner : MonoBehaviour
{
    public Transform enemySpawner;
    public GameObject player;

    public GameObject[] enemyCarPrefab;
    public GameObject newEnemyCar;
    public GameObject[] enemies;

    [SerializeField] private float enemySpawnDelay = 1f;
    [SerializeField] private float[] enemyLane = new float[] { -7.4f, -2.5f, 2.6f, 7.5f };

    private int laneNumber;
    private int previousLaneNumber = 0;
    private float timer;
    private int playerSpeed;
    private int enemySpeed;
    private int randomIndex;

    private Boolean danger = false;

    private Vector3 randomPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        timer = enemySpawnDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("PlayerCar");
        double velocity = player.GetComponent<Rigidbody>().linearVelocity.magnitude * 3.6f;
        playerSpeed = Convert.ToInt32(velocity);

        enemySpawner.position = new Vector3(enemySpawner.position.x, enemySpawner.position.y, player.transform.position.z + 150);

        enemies = GameObject.FindGameObjectsWithTag("EnemyCar");
        Debug.Log("Enemy count: " + enemies.Length);

        foreach(GameObject enemy in enemies)
        {
            //spawner safety check
            if(enemy.transform.position.z > player.transform.position.z + 143)
            {
                danger = true;
            }
            else
            {
                danger = false;
            }

            //destroy enemy cars
            if (enemy.transform.position.z < player.transform.position.z - 150 || enemy.transform.position.z > player.transform.position.z + 750)
            {
                Destroy(enemy);
            }
        }

        if (playerSpeed >= 40)
        {

            //spawn enemy car
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                laneNumber = Random.Range(0, 4);

                if (danger)
                {
                    Debug.Log("danger");
                }
                else
                {
                    enemySpeed = Random.Range(4, 8) * 15;
                    randomIndex = Random.Range(0, enemyCarPrefab.Length);
                    randomPosition = new Vector3(enemyLane[laneNumber], transform.position.y, enemySpawner.position.z);
                    newEnemyCar = Instantiate(enemyCarPrefab[randomIndex], randomPosition, transform.rotation);
                    newEnemyCar.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, enemySpeed / 3.6f);
                    //Debug.Log("my speed" + newEnemyCar.GetComponent<Rigidbody>().velocity.magnitude * 3.6f);
                    /*Debug.Log("safe");
                    Debug.Log("x:" + enemyLane[laneNumber]);
                    Debug.Log("z:" + enemySpawner.position.z + 8);*/
                }

                previousLaneNumber = laneNumber;
                timer = Random.Range(0, 2);
            }
        }
    }
}
