using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UpdateCoin());
    }


    private IEnumerator UpdateCoin()
    { 
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + Vector3.up * 2f;

        yield return AnimateBlock(currentPosition, newPosition);
        yield return AnimateBlock(newPosition, currentPosition);

        Destroy(gameObject);
    }

    private IEnumerator AnimateBlock(Vector3 origin, Vector3 dest)
    {
        float elasped = 0f;
        float duration = 0.25f;

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
