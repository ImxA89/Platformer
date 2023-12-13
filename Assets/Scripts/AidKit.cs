using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField][Min(0)] private int _healPower;

    public int HealPower => _healPower;
}
