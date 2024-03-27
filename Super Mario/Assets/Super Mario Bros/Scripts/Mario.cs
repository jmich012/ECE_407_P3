using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour
{
    public SpriteRenderer bigMarioRenderer;
    public SpriteRenderer smallMarioRenderer;
    public Animator bigMarioAnimator;
    public Animator smallMarioAnimator;
    public AudioSource bgMusic;
    public AudioClip growAudio;
    public AudioClip deathAudio;
    public AudioClip oneUp;
    public AudioClip starPower;
    public AudioClip coinPickUp;

    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer activeRenderer;
    private Animator activeAnimator;
    private PlayerControl PlayerControl;
    private AudioSource audioSource;

    public bool big => bigMarioAnimator.enabled;
    public bool small => smallMarioAnimator.enabled;
    public bool invincible { get; private set;}

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        PlayerControl = GetComponent<PlayerControl>();
        audioSource = GetComponent<AudioSource>();
        activeAnimator = smallMarioAnimator;
        activeRenderer = smallMarioRenderer;
    }

    private void Update()
    {
        activeAnimator.SetBool("Running", PlayerControl.running);
        activeAnimator.SetBool("Jumping", PlayerControl.jump);
        activeAnimator.SetBool("Slide", PlayerControl.sliding);
    }
    public void Hit()
    {
        if (!invincible)
        {
            if (big)
            {
                Shrink();
            }
            else if (small)
            {
                Death();
            }
        }
    }

    public void Grow()
    {
        if (small)
        {

            audioSource.clip = growAudio;
            audioSource.Play();

            smallMarioRenderer.enabled = false;
            bigMarioRenderer.enabled = true;
            smallMarioAnimator.enabled = false;
            bigMarioAnimator.enabled = true;
            activeAnimator = bigMarioAnimator;
            activeRenderer = bigMarioRenderer;

            StartCoroutine(Animate());

            capsuleCollider.offset = new Vector2(0.0f, 0.5f);
            capsuleCollider.size = new Vector2(1.0f, 2.0f);

            PlayerControl.SpeedBoost();
        }
    }

    private void Shrink()
    {
        if (big)
        {
            bigMarioRenderer.enabled = false;
            smallMarioRenderer.enabled = true;
            bigMarioAnimator.enabled = false;
            smallMarioAnimator.enabled = true;
            activeAnimator = smallMarioAnimator;
            activeRenderer = smallMarioRenderer;

            StartCoroutine(Animate());

            capsuleCollider.offset = new Vector2(0.0f, 0.0f);
            capsuleCollider.size = new Vector2(1.0f, 1.0f);

            PlayerControl.ResetSpeed();
        }
    }

    private void Death()
    {
        activeAnimator.SetBool("Dead", true);

        GameObject.Find("BGM").GetComponent<AudioSource>().Pause();

        audioSource.clip = deathAudio;
        audioSource.Play();

        StartCoroutine(DeathAnimation());

        capsuleCollider.enabled = false;

        LevelManager.Instance.ResetScene(3.0f);
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) 
            {
                smallMarioRenderer.enabled = !smallMarioRenderer.enabled;
                bigMarioRenderer.enabled = !smallMarioRenderer.enabled;
            }

            yield return null;
        }

        smallMarioRenderer.enabled = false;
        bigMarioRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    private IEnumerator DeathAnimation() 
    {
        float elapsed = 0f;
        float duration = 3f;

        float gravity = -30f;

        Vector3 velocity = new Vector3(1,10,-11);

        while (elapsed < duration)
        {
            PlayerControl.transform.localPosition += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

    }

    public void StarPower()
    {
        float pauseTime = bgMusic.time;
        bgMusic.Pause();

        AudioClip clip = bgMusic.clip;
        bgMusic.clip = starPower;

        StartCoroutine(StarPowerAnimation(pauseTime,clip));
    }

    private IEnumerator StarPowerAnimation(float playBackTime, AudioClip clip)
    {
        bgMusic.Play();
        invincible = true;

        float elapsed = 0f;

        while (elapsed < 10f) 
        { 
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }
        invincible = false;
        activeRenderer.color = Color.white;
        bgMusic.Stop();

        bgMusic.clip = clip;
        bgMusic.time = playBackTime;
        bgMusic.Play();
    }
    public void PlayOneUp() 
    {
        audioSource.clip = oneUp;
        audioSource.Play();
    }

    public void PickUpCoin()
    { 
        audioSource.clip = coinPickUp;
        audioSource.Play();
    }
}
