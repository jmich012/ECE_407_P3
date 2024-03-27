using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    { 
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUpPowerUP(collision.gameObject);
        }
    }

    private void PickUpPowerUP(GameObject player) 
    {
        switch (type)
        {
            case Type.Coin:
                LevelManager.Instance.AddCoin(1);
                player.GetComponent<Mario>().PickUpCoin();
                break;
            case Type.ExtraLife:
                LevelManager.Instance.AddLife();
                player.GetComponent<Mario>().PlayOneUp();
                break;
            case Type.MagicMushroom:
                player.GetComponent<Mario>().Grow();
                break;
            case Type.Starpower:
                player.GetComponent<Mario>().StarPower();
                break;
        }
        Destroy(gameObject);
    }
}
