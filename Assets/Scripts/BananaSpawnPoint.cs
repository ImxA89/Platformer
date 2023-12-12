using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BananaSpawnPoint : MonoBehaviour
{
    private Banana _banana;
    private int _delayTime = 1000;

    public event Action<Transform> PlayerEntered;

    public void TakeBanana(Banana banana)
    {
        if (banana.transform.position == transform.position)
            _banana = banana;
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            await Task.Delay(_delayTime);

            if (_banana == null)
                PlayerEntered?.Invoke(transform);
        }
    }
}
