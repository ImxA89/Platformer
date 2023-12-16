using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISlowHealthBar : MonoBehaviour
{
    [SerializeField, GameObjectOfType(typeof(IAttackable))] private GameObject _gameObjectWithHealth;
    [SerializeField] private Slider _healthBar;

    private IAttackable _health;
    private Coroutine _changeSliderRoutine;
    private int _maxHealth;
    private float _valueChangerOneStep = 0.1f;

    private void Awake()
    {
        _health = _gameObjectWithHealth.GetComponent<IAttackable>();
        _maxHealth = _health.MaxHealth;
        _healthBar.minValue = 0f;
        _healthBar.maxValue = 1f;
    }

    private void Start()
    {
        _healthBar.value = _maxHealth / _maxHealth;
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth)
    {
        if (_changeSliderRoutine != null)
            StopCoroutine(_changeSliderRoutine);

        _changeSliderRoutine = StartCoroutine(ChangeSliderValue((float)currentHealth / _maxHealth));
    }

    private IEnumerator ChangeSliderValue(float targetValue)
    {
        while (enabled && _healthBar.value != targetValue)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, targetValue, _valueChangerOneStep * Time.deltaTime);
            yield return null;
        }
    }
}
