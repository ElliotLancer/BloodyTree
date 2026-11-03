using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health;
    [SerializeField] private GameObject _damagePrefab;
    [SerializeField] private Transform _damagePopupPoint;
    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;
        _health -= damage;

        GameObject popup = Instantiate(_damagePrefab, _damagePopupPoint.position, Quaternion.identity);
        DamageUI damageUI = popup.GetComponent<DamageUI>();
        if (damageUI != null)
        {
            damageUI.SetDamageText(damage);
            damageUI.SetUIRotation(_damagePopupPoint.position);
            Debug.Log("set popup");
        }


        _health = Mathf.Max(_health, 0);
        if (_health == 0)
            Die();
    }
    private void Die()
    {
        Debug.Log("Zombie died");
        Destroy(gameObject);
    }
}
