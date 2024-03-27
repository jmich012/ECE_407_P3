using UnityEngine;

public class Koopa : MonoBehaviour
{

    private Animator anim;
    private AudioSource AudioSource;
    private bool dead = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Mario mario = collision.gameObject.GetComponent<Mario>();

            if (mario.invincible)
            {
                Hit();
            }
            else if (collision.transform.directionTest(transform, Vector2.down))
            {
                Hit();
            }
            else
            {
                mario.Hit();
            }
        }
    }

    private void Hit()
    {
        dead = true;
        anim.SetBool("Dead", dead);
        AudioSource.Play();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        LevelManager.Instance.KillGoomba();
        Destroy(gameObject, 0.25f);
    }
}
