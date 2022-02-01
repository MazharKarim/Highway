using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeButton : MonoBehaviour
{
    public static bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown()
    {
        BrakeButton.stop = true;
    }

    public void OnPointerUp()
    {
        BrakeButton.stop = false;
    }
}
