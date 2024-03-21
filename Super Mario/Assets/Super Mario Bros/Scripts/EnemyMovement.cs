using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Adjust this to control Goomba's movement speed
    public float activationDistance = 5f; // Distance from camera to activate movement

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isActivated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isActivated && IsVisibleFromCamera())
        {
            isActivated = true;
            ChooseDirection();
        }

        if (isActivated)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe")
        {
            // Change direction when hitting a wall or obstacle
            moveDirection.x *= -1;
        }
    }

    void ChooseDirection()
    {
        // Generate a random direction
        float randomX = Random.Range(-1f, 1f);
        float randomY = 0f;

        // Normalize the direction to make it a unit vector
        moveDirection = new Vector2(randomX, randomY).normalized;
    }

    bool IsVisibleFromCamera()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1);
    }
}
