using UnityEngine;
using UnityEngine.UI;

public class Goomba : MonoBehaviour
{
    public GameObject player;

    private Animator anim;

    private bool dead = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (DotTest(collision.transform,transform,Vector2.down))
            {
                dead = true;
                anim.SetBool("Dead", dead);
                Destroy(gameObject, 0.5f);
            }
            else
            {
                anim.SetBool("Dead", dead);
            }
        }
    }

    // Check if the player is falling onto the Goomba
    private bool DotTest(Transform player, Transform goomba, Vector2 testDriection)
    {
        Vector2 direction = goomba.position - player.position;
        return Vector2.Dot(direction.normalized, testDriection) > 0.25f;
    }
}
