using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousTrack : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private Transform checkpoint ;
    [SerializeField] private Transform track1;
    [SerializeField] private Transform track2;
    private Vector3 moveTrack = new Vector3 (0, 0, 2000);
    private Vector3 moveCheckpoint = new Vector3 (0, 0, 1000);

    private int flag = 0;

    void OnTriggerEnter(Collider collided)
    {
        //Debug.Log(collided.tag);
        if (collided.CompareTag("Player"))
        {
            if (flag == 0)
            {
                //Debug.Log("collision: " + flag);
                trigger.SetActive(false);
                track1.position += moveTrack;
                checkpoint.position += moveCheckpoint;
                Invoke(nameof(ResetTrigger), 2);
                //Debug.Log("flag: " + flag);
            }
            if (flag == 1)
            {
                //Debug.Log("collision: " + flag);
                trigger.SetActive(false);
                track2.position += moveTrack;
                checkpoint.position += moveCheckpoint;
                Invoke(nameof(ResetTrigger), 2);
                //Debug.Log("flag: " + flag);
            }
        }
    }

    public void ResetTrigger() 
    {
        if (flag == 0)
        {
            trigger.SetActive(true);
            flag = 1;
        }
        else if (flag == 1)
        {
            trigger.SetActive(true);
            flag = 0;
        }
    }
}
