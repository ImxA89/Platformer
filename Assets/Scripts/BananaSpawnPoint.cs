using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BananaSpawnPoint : MonoBehaviour
{
    private Banana _bananaOnThisPoint;

    public event Action<Transform> PlayerEntered;

    public void TakeBanana(Banana banana)
    {
        if (banana.transform.position == transform.position)
            _bananaOnThisPoint = banana;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (_bananaOnThisPoint == null)
                PlayerEntered?.Invoke(transform);
        }
    }
}
