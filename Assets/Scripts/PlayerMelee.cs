using System.Threading;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Transform _meleePoint;
    [SerializeField] private float _meleeAttackLength;
    [SerializeField] private LayerMask _masks;
    [SerializeField] private float _coolDown = 2f;
    private float _timer;
    private void Update()
    {
        _timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (_timer <= 0f)
            {
                RaycastHit hit;
                if (Physics.Raycast(_meleePoint.position, _meleePoint.forward, out hit, _meleeAttackLength, _masks))
                {
                    IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        damagable.TakeDamage(_damage);
                    }
                }
                _timer = _coolDown;
            }
        }
    }
}
