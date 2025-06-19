using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeLane : MonoBehaviour
{
    private int direction;
    private int turnDirection;
    private int coOrdinate;
    private int laneNumber;
    private int currentLaneNumber;
    private int newX;
    private float timer;
    private int speed;
    private bool turning = true;

    public GameObject[] enemies;
    private Vector3 myPosition;

    [SerializeField] private float[] enemyLane = new float[] { -7.4f, -2.5f, 2.6f, 7.5f };

    [SerializeField] private List<Wheel> wheels;

    // Start is called before the first frame update
    void Start()
    {
        double velocity = GetComponent<Rigidbody>().linearVelocity.magnitude * 3.6;
        //speed = Convert.ToInt32(velocity);
        timer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //Debug.Log("timer: " + timer);
        if (timer <= 0 && turning)
        {
            myPosition = transform.position;
            currentLaneNumber = GetLaneNumber(myPosition);
            turnDirection = DecideDirection(currentLaneNumber);

            newX = currentLaneNumber + turnDirection;
            timer = Random.Range(10, 15);
        }

        if (transform.position.x > (enemyLane[newX] - 0.1) && transform.position.x < (enemyLane[newX] + 0.1))
        {
            //Debug.Log("turning stopped at " + transform.position.x);
            turnDirection = 0;
        }

        if (turning == false)
        {
            turnDirection = 0;
        }

        Turn(turnDirection, speed);
    }

    void OnTriggerStay(Collider collided)
    {
        //Debug.Log(collided.tag);
        if (collided.CompareTag("Enemy"))
        {
            turning = false;
            //Debug.Log(collided.tag);
        }
    }

    void OnTriggerExit(Collider collided)
    {
        //Debug.Log(collided.tag);
        if (collided.CompareTag("Enemy"))
        {
            turning = true;
            //Debug.Log(collided.tag);
        }
    }

    private int GetLaneNumber(Vector3 position)
    {
        if (position.x > -7.5 && position.x < -7.3)
        {
            laneNumber = 0;
            //Debug.Log("laneNumber: 0 ");
        }
        if (position.x > -2.6 && position.x < -2.4)
        {
            laneNumber = 1;
            //Debug.Log("laneNumber: 1 ");
        }
        if (position.x > 2.5 && position.x < 2.7)
        {
            laneNumber = 2;
            //Debug.Log("laneNumber: 2 ");
        }
        if (position.x > 7.4 && position.x < 7.6)
        {
            laneNumber = 3;
            //Debug.Log("laneNumber: 3 ");
        }

        return laneNumber;
    }

    private int DecideDirection(int lane)
    {
        if (lane == 0)
        {
            direction = Random.Range(0, 2);
            //Debug.Log("direction: " + direction);
        }
        if (lane == 1)
        {
            direction = Random.Range(-1, 2);
            //Debug.Log("direction: " + direction);
        }
        if (lane == 2)
        {
            direction = Random.Range(-1, 2);
            //Debug.Log("direction: " + direction);
        }
        if (lane == 3)
        {
            direction = Random.Range(-1, 1);
            //Debug.Log("direction: " + direction);
        }

        return direction;
    }

    private void Turn(int direction, int speed)
    {
        turning = true;

        //Debug.Log("turn");

        //Debug.Log("x: " + transform.position.x);
        foreach (var wheel in wheels)
        {
            //if (wheel.axel == Axel.Front)
            //while (turning)
            {
                var _steerAngle = 30 * direction;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, 10f * Time.deltaTime);
                //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed / 3.6f);
                //Debug.Log("turning" + transform.position.x);
            }
        }
    }
}
