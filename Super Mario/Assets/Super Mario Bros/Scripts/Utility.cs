using UnityEngine;

public static class Utility
{
    public static bool directionTest(this Transform player, Transform goomba, Vector2 testDriection)
    {
        Vector2 direction = goomba.position - player.position;
        return Vector2.Dot(direction.normalized, testDriection) > 0.25f;
    }
}
