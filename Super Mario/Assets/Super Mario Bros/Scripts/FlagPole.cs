using UnityEngine;
using System.Collections;


public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform bottom;
    public Transform castle;
    public float speed = 6f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag,bottom.position));
            StartCoroutine(LevelComplete(collision.transform));
        }
    }


    private IEnumerator LevelComplete(Transform mario) 
    {
        mario.GetComponent<PlayerControl>().enabled = false;

        GameObject.Find("BGM").GetComponent<AudioSource>().Pause();
        GetComponent<AudioSource>().enabled = true;
        GetComponent<AudioSource>().Play();

        yield return MoveTo(mario, bottom.position);
        yield return MoveTo(mario, mario.position + Vector3.right);
        yield return MoveTo(mario, mario.position + Vector3.right + Vector3.down);
        yield return MoveTo(mario, castle.position);

        mario.gameObject.SetActive(false);
    }


    private IEnumerator MoveTo(Transform transform, Vector3 pos)
    {

        while (Vector3.Distance(transform.position,pos) > 0.125f)
        { 
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = pos;
    }
}
