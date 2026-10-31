using UnityEngine;

public class HealingItem : MonoBehaviour, IUsable
{
    [SerializeField] private int _healAmount;
    [SerializeField] private PlayerHealth _playerHealth;
    public void Use()
    {
        _playerHealth.Heal(_healAmount);
        Destroy(gameObject);
    }
}
