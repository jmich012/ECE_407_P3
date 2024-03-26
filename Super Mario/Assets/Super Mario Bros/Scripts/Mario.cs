using UnityEngine;

public class Mario : MonoBehaviour
{
    public SpriteRenderer bigMarioRenderer;
    public SpriteRenderer smallMarioRenderer;
    public Animator bigMarioAnimator;
    public Animator smallMarioAnimator;

    public bool big => bigMarioRenderer.enabled;
    private bool small => smallMarioRenderer.enabled;

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
        
    }

    private void Shrink()
    { 
    
    }

    private void Death()
    { 
        smallMarioRenderer.enabled = false;
        bigMarioRenderer.enabled = false;

        // TODO reset level
    }

    private void Animate()
    { }

}
