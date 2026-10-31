using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Ragdoll _ragdoll;

    public event Action OnHealthChanged;
    public event Action OnPlayerDeath;
    public float Health => _currentHealth;
    public float MaxHealth => _maxHealth;
    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        Debug.Log("Player took damage");
        if (_currentHealth == 0)
            Die();
        OnHealthChanged?.Invoke();
    }
    public void Heal(int amount)
    {
        _currentHealth += amount;
        OnHealthChanged?.Invoke();
    }
    private void Die()
    {
        Debug.Log("Player died");
        _ragdoll.EnableRagdoll();
        OnPlayerDeath?.Invoke();
    }
}
