using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BananaSpawnPoint : MonoBehaviour
{
    public event Action<Transform> PlayerEntered;

    private Banana _banana;
    private int _delayTime = 1000;

    public void TakeBanana(Banana banana)
    {
        if (banana.GetComponent<Transform>().position == transform.position)
            _banana = banana;
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() != null)
        {
            await Task.Delay(_delayTime);

            if (_banana == null)
                PlayerEntered?.Invoke(transform);
        }
    }
}
