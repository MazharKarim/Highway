using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum Axel
{
    Front,
    Rear
}

[Serializable]
public struct Wheel
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel;
}

public class CarController : MonoBehaviour
{
    [SerializeField] private float maxAcceleration = 300.0f;
    [SerializeField] private float turnSensitivity = 1.0f;
    [SerializeField] private float turnSmootness = 10f;
    [SerializeField] private float maxSteerAngle = 30.0f;
    [SerializeField] private float enginePower = 300.0f;
    [SerializeField] private int topSpeed = 180;
    [SerializeField] private List<Wheel> wheels;
    [SerializeField] private float brakeForce;
    [SerializeField] private Vector3 _centerOfMass;

    private float currentbrakeForce;
    private bool isBraking;

    private float inputX, inputY;
    public static float turn = 0;

    private int speed = 0;

    private Rigidbody _rb;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        AnimateWheels();
        GetInputs();
        Accelerate();
        Turn();
        LockCarRotation();
    }

    private void LateUpdate()
    {

    }

    private void GetInputs()
    {
        /*    PC: Arrow    */
        /*inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);*/
        /*
                Debug.Log("inputX: " + inputX);
                Debug.Log("inputY: " + inputY);
                Debug.Log("isBraking: " + isBraking);*/
        /*    Mobile: Accelerometer    */
        /*
        inputX = Input.acceleration.x * 2;
        inputY = AccelerateButton.forword;
        isBraking = BrakeButton.stop;
        */


        /*    Mobile: Buttons    */

        inputX = turn;
        inputY = AccelerateButton.forword;
        isBraking = BrakeButton.stop;

    }

    private void Accelerate()
    {
        foreach (var wheel in wheels)
        {
            double velocity = GetComponent<Rigidbody>().linearVelocity.magnitude * 3.6;
            speed = Convert.ToInt32(velocity);

            if (speed <= topSpeed)
            {
                wheel.collider.motorTorque = inputY * maxAcceleration * enginePower * Time.deltaTime;
                //Turn();
            }
            else
            {
                wheel.collider.motorTorque = 0;
                //Turn();
            }

            // Debug.Log("torque: " + wheel.collider.motorTorque);
            //  Debug.Log("s: " + speed);
            //  Debug.Log("y: " + inputY);
            // Debug.Log("t: " + Time.deltaTime);
        }

        currentbrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.brakeTorque = currentbrakeForce;
            //Debug.Log(currentbrakeForce);
        }
    }

    private void Turn()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = inputX * turnSensitivity * maxSteerAngle;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, turnSmootness * Time.deltaTime);
            }
        }
    }

    private void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion _rot;
            Vector3 _pos;

            wheel.collider.GetWorldPose(out _pos, out _rot);
            wheel.model.transform.position = _pos;
            wheel.model.transform.rotation = _rot;

            // TO LOCK REAR WHEEL ROTATION
            if (wheel.axel == Axel.Rear)
            {
                _rot.y = 0;
                _rot.z = 0;
                // wheel.model.transform.SetPositionAndRotation(_pos, _rot);
            }
        }
    }

    private void LockCarRotation()
    {
        Vector3 carRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(carRotation.x, 0, carRotation.z);
    }
}
