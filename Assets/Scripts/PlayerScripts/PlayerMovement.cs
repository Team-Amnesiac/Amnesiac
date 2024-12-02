using UnityEngine;
using UnityEngine.SceneManagement;

// This class manages the player's movement, jumping, and interaction with the game world.
// It includes walking, running, jumping mechanics, and basic combat animations.

public class PlayerMovement : MonoBehaviour
{
    // Movement speed of the player.
    public float speed = 5f;
    // Height of the player's jump.
    public float jumpHeight = 1f;
    // Duration of the jump animation.
    public float jumpDuration = 1f;

    // Reference to the player's Animator component for controlling animations.
    private Animator animator;
    // Reference to the camera's transform for directional movement.
    public Transform cameraTransform;

    // Tracks whether the player is currently jumping.
    private bool isJumping = false;
    // Timestamp when the jump started.
    private float jumpStartTime;
    // Initial position of the player before the jump.
    private Vector3 startPosition;

    // Initializes player state and sets references.
    void Start()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Play); 
        // Set the game state to "Play" at the start.

        animator = GetComponent<Animator>(); // Get the Animator component attached to the player.

        PlayerManager.Instance.setPlayerGameObject(this.gameObject); 
        // Register the player GameObject with the PlayerManager.

        if (Camera.main != null) // Ensure the main camera exists.
        {
            cameraTransform = Camera.main.transform; // Set the camera transform for movement orientation.
        }
    }

    // Handles player input and movement logic.
    void Update()
    {
        // Get input from the horizontal and vertical axes (WASD/Arrow keys).
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized; 
        // Normalize the input vector to ensure consistent movement speed.

        if (direction.magnitude >= 0.1f) // Check if there is meaningful input.
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y; 
            // Calculate the target rotation angle based on input and camera orientation.

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f); 
            // Rotate the player toward the movement direction.

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
            // Convert the target angle into a forward movement vector.

            transform.Translate(moveDir * speed * Time.deltaTime, Space.World); 
            // Move the player in the calculated direction.

            animator.SetFloat("Speed", direction.magnitude); 
            // Update the Animator's "Speed" parameter to trigger running animations.
        }
        else
        {
            animator.SetFloat("Speed", 0); 
            // Set "Speed" to 0 to stop running animations when there's no input.
        }

        if (Input.GetButtonDown("Jump") && !isJumping) // Check for jump input and ensure the player isn't already jumping.
        {
            StartJump(); // Begin the jump process.
        }

        if (isJumping) // If the player is mid-jump, update the jump motion.
        {
            PerformJump();
        }

        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click for basic attack.
        {
            animator.SetTrigger("Hit"); // Trigger the attack animation.
        }
    }

    // Handles collisions with other objects.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") || other.CompareTag("Relic")) // Skip processing for items or relics.
        {
            Debug.Log($"Skipping item trigger: {other.name}");
            return;
        }

        if (other.CompareTag("Enemy")) // Check if the player collided with an enemy.
        {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>(); // Get the EnemyAI component of the enemy.
            Debug.Log("Player engaged an enemy, transitioning to battle scene...");
            BattleManager.Instance.initializeBattle(BattleManager.Attacker.Player, enemy); 
            // Initialize the battle with the player attacking first.
        }
    }

    // Starts the jump animation and logic.
    void StartJump()
    {
        isJumping = true; // Mark the player as jumping.
        jumpStartTime = Time.time; // Record the time when the jump started.
        startPosition = transform.position; // Store the player's initial position.

        animator.SetTrigger("Jump"); // Trigger the jump animation.
    }

    // Handles the jump motion over time.
    void PerformJump()
    {
        float elapsed = Time.time - jumpStartTime; // Calculate the elapsed time since the jump started.
        float normalizedTime = elapsed / jumpDuration; 
        // Normalize the elapsed time to fit within the jump duration.

        if (normalizedTime <= 1f) // If the jump is still in progress.
        {
            float jumpProgress = Mathf.Sin(Mathf.PI * normalizedTime); 
            // Calculate the vertical progress of the jump using a sine wave for a smooth motion.

            float newY = startPosition.y + jumpHeight * jumpProgress; 
            // Calculate the new Y position based on the jump progress.

            transform.position = new Vector3(transform.position.x, newY, transform.position.z); 
            // Apply the new position to the player.
        }
        else // If the jump duration is complete.
        {
            isJumping = false; // Mark the player as no longer jumping.
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z); 
            // Reset the player's Y position to the initial value.
        }
    }
}
