using UnityEngine;
using UnityEngine.UI;

public class Goomba: MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;
    private bool dead = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
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
        if (dead) 
        {
            audioSource.enabled = true;
            audioSource.Play();
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        LevelManager.Instance.KillGoomba();
        Destroy(gameObject, 0.25f);
    }
}
