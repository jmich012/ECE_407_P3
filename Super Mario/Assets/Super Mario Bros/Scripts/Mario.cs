using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour
{
    public SpriteRenderer bigMarioRenderer;
    public SpriteRenderer smallMarioRenderer;
    public Animator bigMarioAnimator;
    public Animator smallMarioAnimator;

    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer activeRenderer;
    private Animator activeAnimator;
    private PlayerControl PlayerControl;

    public bool big => bigMarioAnimator.enabled;
    public bool small => smallMarioAnimator.enabled;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        PlayerControl = GetComponent<PlayerControl>();
        activeAnimator = smallMarioAnimator;
    }

    private void Update()
    {
        activeAnimator.SetBool("Running", PlayerControl.running);
        activeAnimator.SetBool("Jumping", PlayerControl.jump);
        activeAnimator.SetBool("Slide", PlayerControl.sliding);
    }
    public void Hit()
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

    public void Grow()
    {
        if (small)
        {
            smallMarioRenderer.enabled = false;
            bigMarioRenderer.enabled = true;
            smallMarioAnimator.enabled = false;
            bigMarioAnimator.enabled = true;
            activeAnimator = bigMarioAnimator;
            activeRenderer = bigMarioRenderer;

            StartCoroutine(Animate());

            capsuleCollider.offset = new Vector2(0.0f, 0.5f);
            capsuleCollider.size = new Vector2(1.0f, 2.0f);
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
        }
    }

    private void Death()
    {
        activeAnimator.SetBool("Dead", true);

        bigMarioRenderer.enabled = false;
        bigMarioAnimator.enabled = false;
        

        smallMarioAnimator.enabled = false;
        smallMarioRenderer.enabled = false;

        LevelManager.Instance.ResetScene(3f);
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

}
