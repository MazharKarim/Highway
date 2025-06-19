using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedoMeter : MonoBehaviour
{
    [SerializeField] private float maxSpeedAngle = -30;
    [SerializeField] private float minSpeedAngle = 210;
    [SerializeField] private float maximumSpeed = 200f;
    [SerializeField] private float labelAmount = 10;

    private Transform needleTransform;
    private Transform speedLabelTemplateTransform;

    private float maxSpeed;
    private int speed;

    public GameObject car;

    private void Awake()
    {
        needleTransform = transform.Find("Needle");
        speedLabelTemplateTransform = transform.Find("SpeedLabelTemplate");
        speedLabelTemplateTransform.gameObject.SetActive(false);

        speed = 0;
        maxSpeed = maximumSpeed;

        CreateSpeedLabel();
    }

    private void FixedUpdate()
    {
        car = GameObject.FindGameObjectWithTag("PlayerCar");

        double velocity = car.GetComponent<Rigidbody>().linearVelocity.magnitude * 3.6;
        speed = Convert.ToInt32(velocity);
        //speed = GetComponent<CarController>().Accelerate();
       // Debug.Log("speed: " + speed);
        //speed += 30f * Time.deltaTime;

        //if (speed > maxSpeed) speed = maxSpeed;

        needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }

    private void CreateSpeedLabel()
    {
        float totalAngleSize = minSpeedAngle - maxSpeedAngle;

        for(int i = 0; i <= labelAmount; i++)
        {
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);
            float labelSpeeedNormalized = (float)i / labelAmount;
            float speedLabelAngle = minSpeedAngle - labelSpeeedNormalized * totalAngleSize;
            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedLabelTransform.Find("SpeedText").GetComponent<Text>().text = Mathf.RoundToInt(labelSpeeedNormalized * maxSpeed).ToString();
            speedLabelTransform.Find("SpeedText").eulerAngles = Vector3.zero;
            speedLabelTransform.gameObject.SetActive(true);
        }

        needleTransform.SetAsLastSibling();
    }

    private float GetSpeedRotation()
    {
        float totalAngleSize = minSpeedAngle - maxSpeedAngle;

        float speedNormalized = speed / maxSpeed;

        return minSpeedAngle - speedNormalized * totalAngleSize;
    }
}
