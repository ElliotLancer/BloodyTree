using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private Transform _explosionPoint;
    [SerializeField] private LayerMask _masks;
    public void StartExplosion()
    {
        Debug.Log("Started");
        Collider[] colliders = Physics.OverlapSphere(_explosionPoint.position, _radius, _masks);
        foreach (Collider col in colliders)
        {
            IDamagable damagableObj = col.GetComponent<IDamagable>();
            if (damagableObj != null)
                damagableObj.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
