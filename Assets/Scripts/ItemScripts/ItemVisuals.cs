using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))] // Ensures the GameObject has a Collider component for trigger detection.

// This class handles the visual effects and player interactions for items in the game world.
// It includes floating and rotating animations for the item and detects when the player is nearby.

public class ItemVisuals : MonoBehaviour
{
    // Height of the floating animation.
    [SerializeField] private float floatHeight = 0.25f;
    // Speed of the rotation animation.
    [SerializeField] private float rotationSpeed = 30.0f;
    // Reference to the associated ItemPickup script for interaction logic.
    [SerializeField] private ItemPickup itemPickup;
    // Stores the initial Y position of the item for floating animation.
    private float initialY;

    // Initializes the item's position.
    void Start()
    {
        initialY = transform.position.y; // Store the initial Y position for use in floating animation.
    }

    // Updates the item's position and rotation every frame.
    void Update()
    {
        Vector3 newPosition = transform.position; // Get the current position.

        newPosition.y = initialY + Mathf.Sin(Time.time) * floatHeight; 
        // Modify the Y position to create a floating effect based on a sine wave.

        transform.position = newPosition; // Apply the floating effect to the item's position.

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self); 
        // Rotate the item around its Y-axis for a spinning effect.
    }

    // Detects when a player enters the item's trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called by: {other.name}"); // Log the object that entered the trigger.

        if (other.CompareTag("Player")) // Check if the object is the player.
        {
            itemPickup.setPlayerNearby(true); // Notify the ItemPickup script that the player is nearby.
            itemPickup.setPlayerAnimator(other.GetComponent<Animator>()); 
            // Set the player's animator for triggering pickup animations.
            Debug.Log("Player is near the item."); // Log that the player is near the item.
        }
    }

    // Detects when a player exits the item's trigger zone.
    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit called by: {other.name}"); // Log the object that exited the trigger.

        if (other.CompareTag("Player")) // Check if the object is the player.
        {
            itemPickup.setPlayerNearby(false); // Notify the ItemPickup script that the player is no longer nearby.
            itemPickup.setPlayerAnimator(null); // Remove the reference to the player's animator.
            Debug.Log("Player left the item."); // Log that the player left the item.
        }
    }
}
