using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    private Rigidbody _rb;
    private float _destroyTime = 3f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Destroy(gameObject, _destroyTime);
    }
    public void ShootBullet(Vector3 direction)
    {
        _rb.linearVelocity = direction * _speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!CheckCollision(other))
            return;

        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
            damagable.TakeDamage(_damage);
    }
    private bool CheckCollision(Collider other)
    {
        return other.CompareTag("Enemy") ||
               other.CompareTag("Vehicle");
    }
}
