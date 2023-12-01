using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action BananaTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Banana>(out Banana banana))
        {
            Destroy(banana.gameObject);
            BananaTaken?.Invoke();
        }
    }
}
