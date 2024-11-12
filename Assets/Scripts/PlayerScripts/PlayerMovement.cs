using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    public Transform cameraTransform;

    void Start()
    {
        animator = GetComponent<Animator>();

        //not necessary anymore
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDir * speed * Time.deltaTime, Space.World);

            animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Hit");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Skip triggers with "Item" tag
        if (other.CompareTag("Item"))
        {
            Debug.Log($"Skipping item trigger: {other.name}");
            return;
        }

        // Process "Enemy" triggers
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player engaged an enemy, transitioning to battle scene...");
            GameManager.Instance.SetGameState(GameManager.GameState.Battle);
        }
    }
}
