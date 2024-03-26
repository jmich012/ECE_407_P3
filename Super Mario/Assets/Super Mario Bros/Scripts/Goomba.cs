using UnityEngine;
using UnityEngine.UI;

public class Goomba: MonoBehaviour
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
            if (collision.transform.directionTest(transform,Vector2.down))
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
}
