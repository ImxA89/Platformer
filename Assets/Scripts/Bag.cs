using UnityEngine;

[RequireComponent(typeof(Player))]
public class Bag : MonoBehaviour
{
    private Player _player;

    public int Bananas {get; private set;}

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.BananaTaken += OnBananaTaken;
    }

    private void OnDisable()
    {
        _player.BananaTaken -= OnBananaTaken;
    }

    private void OnBananaTaken()
    {
        Bananas++;
    }
}
