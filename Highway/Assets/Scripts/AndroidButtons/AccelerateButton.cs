using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateButton : MonoBehaviour
{
    public static int forword = 0;

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
        AccelerateButton.forword = 1;
    }

    public void OnPointerUp()
    {
        AccelerateButton.forword = 0;
    }
}
