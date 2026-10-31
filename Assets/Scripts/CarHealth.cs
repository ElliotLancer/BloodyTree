using UnityEngine;

public class CarHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health;
    [SerializeField] private Material _carMaterial;
    [SerializeField] private Car _cam;
    public void Awake()
    {
        _carMaterial.color = Color.white;
    }
    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;
        _health -= damage;
        Debug.Log("car took damage");

        _health = Mathf.Max(_health, 0);
        if (_health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _carMaterial.color = Color.black;
        _cam.CarDestroied();
        Debug.Log("car died");
    }

}
