using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class EnemyCarController : MonoBehaviour
{
    [SerializeField] private List<Wheel> wheels;
    [SerializeField] private float brakeForce;
    [SerializeField] private Vector3 _centerOfMass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimateWheels();
        LockCarRotation();
    }

    public void ApplyBraking()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.brakeTorque = brakeForce;
            //Debug.Log(currentbrakeForce);
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

            if (wheel.axel == Axel.Rear)
            {
                _rot.y = 0;
                _rot.z = 0;
                wheel.model.transform.SetPositionAndRotation(_pos, _rot);
            }
        }
    }

    private void LockCarRotation()
    {
        Vector3 carRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(carRotation.x, 0, carRotation.z);
    }
}
