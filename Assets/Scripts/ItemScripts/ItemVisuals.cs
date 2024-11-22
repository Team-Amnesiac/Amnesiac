using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class ItemVisuals : MonoBehaviour
{
    [SerializeField] private float floatHeight   = 0.25f;
    [SerializeField] private float rotationSpeed = 30.0f;
    private float initialY;
    private ItemPickup itemPickup;


    void Start()
    {
        initialY = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called by: {other.name}");

        if (other.CompareTag("Player"))
        {
            itemPickup.SetPlayerNearby(true);
            itemPickup.SetPlayerAnimator(other.GetComponent<Animator>());
            Debug.Log("Player is near the item.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit called by: {other.name}");

        if (other.CompareTag("Player"))
        {
            itemPickup.SetPlayerNearby(false);
            itemPickup.SetPlayerAnimator(null);
            Debug.Log("Player left the item.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;

        newPosition.y = initialY + Mathf.Sin(Time.time) * floatHeight;

        transform.position = newPosition;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    public void SetItemPickup(ItemPickup itemPickup)
    {
        this.itemPickup = itemPickup;
    }
}
