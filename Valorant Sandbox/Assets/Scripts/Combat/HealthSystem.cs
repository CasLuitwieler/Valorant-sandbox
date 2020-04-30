using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;

    private float _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        _health = Mathf.Max(_health, 0f);
    }
}
