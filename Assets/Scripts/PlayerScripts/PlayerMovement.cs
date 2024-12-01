using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 1f;
    public float jumpDuration = 1f;
    private Animator animator;
    public Transform cameraTransform;

    private bool isJumping = false;
    private float jumpStartTime;
    private Vector3 startPosition;

    void Start()
    {
        animator = GetComponent<Animator>();

        PlayerManager.Instance.setPlayerGameObject(this.gameObject);

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

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            StartJump();
        }

        if (isJumping)
        {
            PerformJump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Hit");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") || other.CompareTag("Relic"))
        {
            Debug.Log($"Skipping item trigger: {other.name}");
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            Debug.Log("Player engaged an enemy, transitioning to battle scene...");
            BattleManager.Instance.initializeBattle(BattleManager.Attacker.Player, enemy);
        }

        if (other.tag == "LevelExit")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void StartJump()
    {
        isJumping = true;
        jumpStartTime = Time.time;
        startPosition = transform.position;

        animator.SetTrigger("Jump");
    }

    void PerformJump()
    {
        float elapsed = Time.time - jumpStartTime;
        float normalizedTime = elapsed / jumpDuration;

        if (normalizedTime <= 1f)
        {
            float jumpProgress = Mathf.Sin(Mathf.PI * normalizedTime);
            float newY = startPosition.y + jumpHeight * jumpProgress;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else
        {
            isJumping = false;
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
        }
    }
}
