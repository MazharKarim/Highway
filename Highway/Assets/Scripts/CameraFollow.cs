using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(0, 0, player.position.z);
        transform.position = targetPosition + offset;
    }
}
