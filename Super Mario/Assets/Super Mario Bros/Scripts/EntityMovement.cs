using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float moveSpeed = 1f; 
    public Vector2 moveDirection = Vector2.left;

    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private Transform groundCheck;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("groundCheck");
        enabled = false;
    }

    private void FixedUpdate()
    {
        velocity.x = moveDirection.x * moveSpeed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        bool grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (grounded)
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }


    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnBecameVisible() 
    {
        enabled = true;
    }

    private void OnEnable()
    {
        rigidBody.WakeUp();
    }

    private void OnDisable()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.Sleep();
    }
}
