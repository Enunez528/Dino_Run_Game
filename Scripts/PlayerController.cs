using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform gunTransform; // Assign the gun's transform in the Inspector
    public GameObject bulletPrefab; // Assign the bullet prefab in the Inspector
    public float bulletSpeed = 20f; // Speed of the bullet
    public bool isGrounded;
    public float jumpForce = 5f;
    public AudioClip shootSound; // Assign the shooting audio clip in the Inspector
    public AudioClip popSound; // Assign the popping audio clip in the Inspector
    public string nextSceneName; 
    public GameMaster gameMaster;

    private Rigidbody rbody;
    private Animator anim;
    private AudioSource audioSource;

    // Timer variables
    private float timeElapsed = 0f; // Time elapsed during the game
    private float bestTime = Mathf.Infinity; // Best time (initialize to a very high number)

    public UnityEngine.UI.Text timerText;  // UI Text to display the timer
    public UnityEngine.UI.Text bestTimeText;  // UI Text to display the best time
    

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Ensure an AudioSource is attached to this GameObject

        string bestTimeKey = "BestTime_Level" + SceneSwitcher.LevelCounter;

        if (PlayerPrefs.HasKey(bestTimeKey))
        {
            bestTime = PlayerPrefs.GetFloat(bestTimeKey);
        }

        // Update the best time UI
        UpdateBestTimeUI();
    }

    void Update()
    {
        HandleMouseRotation();
        HandleMovement();
        HandleJump();

        anim.SetBool("isWalking", rbody.velocity.magnitude > 0.1f);

        if (Input.GetKeyDown(KeyCode.F)) // Press 'F' to shoot
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }

        // Increase the timer while the game is running
        timeElapsed += Time.deltaTime;

        // Update the timer text with the formatted time
        if (timerText != null)
        {
            timerText.text = "Time: " + timeElapsed.ToString("F2");
        }
    }

    void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * moveSpeed;
        rbody.velocity = new Vector3(movement.x, rbody.velocity.y, movement.z);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("door"))
        {
            GameMaster gameMaster = FindObjectOfType<GameMaster>();

            // Check if all balloons are popped before winning
            if (gameMaster != null && gameMaster.AreAllBalloonsPopped())
            {
                OnGameWon(); // Save best time
                SceneManager.LoadScene("Win"); // Load the win screen
            }
        }

        // Check for collision with the Net ground
        if (collision.gameObject.name == "Net")
        {
            // Load the specified scene
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Next scene name is not set in the Inspector.");
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Shoot()
    {
        // Create the bullet with the same rotation as the gun
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);

        bullet.transform.rotation = Quaternion.Euler(90, gunTransform.eulerAngles.y, 0);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = gunTransform.forward * bulletSpeed;
        }

        // Play shooting sound
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Add collision logic to bullet
        BulletCollision(bullet);
    }

    void BulletCollision(GameObject bullet)
    {
        // Ensure the bullet has a Collider and Rigidbody
        Collider bulletCollider = bullet.GetComponent<Collider>();
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletCollider == null || bulletRigidbody == null)
        {
            Debug.LogError("Bullet prefab must have a Collider and Rigidbody.");
            return;
        }

        // Detect collision with balloons
        bulletCollider.isTrigger = true; // Ensure the bullet is set as a trigger

        bullet.AddComponent<BulletTrigger>().Initialize(popSound, audioSource);
    }

    void UpdateBestTimeUI()
    {
        if (bestTimeText != null)
        {
            bestTimeText.text = "Best Time: " + (bestTime < Mathf.Infinity ? bestTime.ToString("F2") : "N/A");
        }
    }

    void OnGameWon()
    {
        string bestTimeKey = "BestTime_Level" + SceneSwitcher.LevelCounter;

        // Check if the current time is better than the saved best time
        if (timeElapsed < bestTime)
        {
            bestTime = timeElapsed;

            PlayerPrefs.SetFloat(bestTimeKey, bestTime);

            // Save PlayerPrefs to disk
            PlayerPrefs.Save();

            // Update the best time UI
            UpdateBestTimeUI();
        }
    }

}

// Helper class for handling bullet triggers
public class BulletTrigger : MonoBehaviour
{
    private AudioClip popSound;
    private AudioSource audioSource;

    public void Initialize(AudioClip popSound, AudioSource audioSource)
    {
        this.popSound = popSound;
        this.audioSource = audioSource;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            // Play pop sound
            if (popSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(popSound);
            }

            // Destroy the balloon
            Destroy(other.gameObject);

            // Call the GameMaster's OnBalloonPopped method
            GameMaster gameMaster = FindObjectOfType<GameMaster>();
            if (gameMaster != null)
            {
                gameMaster.OnBalloonPopped();  // Update balloon count in GameMaster
            }

            // Destroy the bullet as well
            Destroy(gameObject);
        }
    }
}
