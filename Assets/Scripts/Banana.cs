using System;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public event Action<Transform> Taken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>()!= null)
        {
            Taken?.Invoke(transform);
        }
    }
}
