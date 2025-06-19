using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("PlayerCar");

        Vector3 targetPosition = new Vector3(0, 0, player.transform.position.z);
        transform.position = targetPosition + offset;
    }
}
