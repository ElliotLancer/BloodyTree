using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackCooldown;
    private PlayerHealth _player;
    private bool _isInAttackZone;
    private bool _canAttack = true;
    public void Attack()
    {
        if (_isInAttackZone && _canAttack)
        {
            StartCoroutine(AttackRoutine());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInAttackZone = true;
            if (_player == null)
                _player = other.GetComponent<PlayerHealth>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInAttackZone = false;
        }
    }
    private IEnumerator AttackRoutine()
    {
        _player.TakeDamage(_damage);
        Debug.Log("Attacked Coroutine");

        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
