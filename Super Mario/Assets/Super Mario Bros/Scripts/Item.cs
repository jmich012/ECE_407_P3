using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SpawnItem());
    }


    private IEnumerator SpawnItem()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D trigger = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rb.isKinematic = true;
        circleCollider.enabled = false;
        trigger.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 origin = transform.localPosition;
        Vector3 end = transform.localPosition + Vector3.up;

        while (elapsed < duration)
        {
            float time = elapsed / duration;

            transform.localPosition = Vector3.Lerp(origin, end, time);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = end;

        rb.isKinematic = false;
        circleCollider.enabled = true;
        trigger.enabled = true;

    }


}
