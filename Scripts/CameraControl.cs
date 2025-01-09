using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public float cameraDistance; // Distance from the player (Adjust in Unity)
    public float cameraHeight; // Height above the player (Adjust in Unity)
    public float smoothSpeed = 5f; // Speed of camera movement

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, cameraHeight, -cameraDistance);
    }

    void LateUpdate()
    {
        // Desired position based on player's rotation and offset
        Vector3 desiredPosition = player.transform.position + player.transform.rotation * offset;

        // Smoothly move camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at the player
        transform.LookAt(player.transform.position + Vector3.up * 1.5f); // Adjust the height if necessary
    }
}
    
