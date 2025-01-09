using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public float distance = 5f; // Total distance to move
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float displacement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + Vector3.right * displacement;
    }
}
