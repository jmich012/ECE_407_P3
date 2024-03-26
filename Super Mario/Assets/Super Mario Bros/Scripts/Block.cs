using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int maxHits = -1;
    public Sprite emptyBlock;
    public bool canBeDestoryed = false;

    private bool blockEmpty = false;
    private bool changing;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!changing && !blockEmpty && collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.directionTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }
    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        
        // decrement the durability of the block
        maxHits--;
        
        // check if the block has anymore hits
        if (maxHits == 0)
        {
            blockEmpty = true;
            spriteRenderer.sprite = emptyBlock;
        }

        // start animation process for block
        StartCoroutine(UpdateBlock());
       
        // if the block disappears from the scene after it's empty
        // destory the block object
        if (canBeDestoryed && blockEmpty)
        {
            Destroy(gameObject);
        }
    }


    private IEnumerator UpdateBlock()
    {
        changing = true;

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + Vector3.up * 0.5f;

        changing = false;

        yield return AnimateBlock(currentPosition, newPosition);
        yield return AnimateBlock(newPosition, currentPosition);
    }

    private IEnumerator AnimateBlock(Vector3 origin, Vector3 dest)
    {
        float elasped = 0f;
        float duration = 0.125f;

        while (elasped < duration)
        {
            float percentOfTimePassed = elasped / duration;

            transform.localPosition = Vector3.Lerp(origin, dest, percentOfTimePassed);
            elasped += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = dest;
    }
}
