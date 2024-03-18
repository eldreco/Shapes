using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] ISpawnable.Type type;

    public ISpawnable.Type GetObstacleType()
    {
        return type;
    }
}
