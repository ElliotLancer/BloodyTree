using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class RaycastHitShot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletInpactPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _masks;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDistance;
    [SerializeField] private float _offset;
    public void ShootRay()
    {
        Ray ray = new Ray (_shootPoint.position, _shootPoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _shootDistance, _masks))
        {
            IDamagable damagable = hit.collider.GetComponent<IDamagable>();

            if (damagable != null)
            {
                damagable.TakeDamage(_damage);
            }
            Vector3 spawnPosition = hit.point + hit.normal * _offset;

            Instantiate(_bulletInpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
