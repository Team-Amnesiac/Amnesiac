using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float move = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(move));
        Vector3 movement = new Vector3(0, 0, move) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }
}
