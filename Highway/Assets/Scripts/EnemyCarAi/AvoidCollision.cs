using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject[] me;

    private float timer;

    private int mySpeed;
    private int changeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        double velocity = transform.parent.GetComponent<Rigidbody>().linearVelocity.magnitude * 3.6f;
        mySpeed = Convert.ToInt32(velocity);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //Debug.Log("timer: " + timer);
        if (timer <= 0)
        {
            if(mySpeed >= 60 && mySpeed <= 90)
            {
                changeSpeed = 1;
            }
            else if (mySpeed > 90)
            {
                changeSpeed = -1;
            }
            else
            {
                changeSpeed = 0;
            }
            mySpeed += (changeSpeed * 10);
            timer = 10;
        }

        transform.parent.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, mySpeed / 3.6f);
    }

    void OnTriggerStay(Collider collided)
    {
        //Debug.Log(collided.tag);
        if (collided.CompareTag("Enemy"))
        {
            double newVelocity = collided.transform.parent.parent.GetComponent<Rigidbody>().linearVelocity.magnitude * 3.6f;
            mySpeed = Convert.ToInt32(newVelocity);
            //Debug.Log("new speed: " + transform.parent.GetComponent<Rigidbody>().velocity.magnitude * 3.6f);
            //Debug.Log(collided.tag);
        }
    }
}
