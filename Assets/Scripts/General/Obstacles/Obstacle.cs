using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] ISpawnable.Type type;

    public ISpawnable.Type GetType()
    {
        return type;
    }
}
