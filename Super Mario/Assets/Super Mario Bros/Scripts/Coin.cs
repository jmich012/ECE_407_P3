using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool inBlock = false;
    private void Start()
    {
        if (inBlock)
        {
            StartCoroutine(UpdateCoin());
            LevelManager.Instance.AddCoin(2);
        }
    }


    private IEnumerator UpdateCoin()
    { 
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + Vector3.up * 2f;

        GetComponent<AudioSource>().Play();

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
