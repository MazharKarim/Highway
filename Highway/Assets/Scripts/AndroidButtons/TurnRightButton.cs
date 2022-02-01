using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRightButton : MonoBehaviour
{
    public static int right = 0;

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
        CarController.turn = 1;
    }

    public void OnPointerUp()
    {
        CarController.turn = 0;
    }
}
