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

                break;
            case Type.ExtraLife: 
                
                break;
            case Type.MagicMushroom:

                break;
            case Type.Starpower:

                break;
        }
        Destroy(gameObject);
    }
}
