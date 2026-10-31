using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Material _material;

    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _attackDistance;

    [SerializeField] private Color _stopColor;
    [SerializeField] private Color _moveColor;
    [SerializeField] private EnemyAttack _attack;

    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector2 vector = new Vector2(_player.position.x - transform.position.x, _player.position.z - transform.position.z);
        float distance = vector.magnitude;
        if (distance > _stopDistance)
        {
            Stop();
        }
        else
        {
            Move();
        }
        if (distance < _attackDistance)
        {
            Stop();
            _attack.Attack();
        }
    }
    private void Move()
    {
        Vector3 dir = (_player.position - transform.position).normalized;
        _rb.linearVelocity = new Vector3(dir.x, _rb.linearVelocity.y, dir.z) * _speed;
        _material.color = _moveColor;
    }
    private void Stop()
    {
        _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
        _material.color = _stopColor;
    }
}
