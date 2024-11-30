using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class ItemVisuals : MonoBehaviour
{
    [SerializeField] private float      floatHeight   = 0.25f;
    [SerializeField] private float      rotationSpeed = 30.0f;
    [SerializeField] private ItemPickup itemPickup;
    private float initialY;


    void Start()
    {
        initialY = transform.position.y;
    }


    void Update()
    {
        Vector3 newPosition = transform.position;

        newPosition.y = initialY + Mathf.Sin(Time.time) * floatHeight;

        transform.position = newPosition;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called by: {other.name}");

        if (other.CompareTag("Player"))
        {
            itemPickup.setPlayerNearby(true);
            itemPickup.setPlayerAnimator(other.GetComponent<Animator>());
            Debug.Log("Player is near the item.");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit called by: {other.name}");

        if (other.CompareTag("Player"))
        {
            itemPickup.setPlayerNearby(false);
            itemPickup.setPlayerAnimator(null);
            Debug.Log("Player left the item.");
        }
    }
}
