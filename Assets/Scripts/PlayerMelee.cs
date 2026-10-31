using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Transform _meleePoint;
    [SerializeField] private float _meleeAttackLength;
    [SerializeField] private LayerMask _masks;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_meleePoint.position, _meleePoint.forward, out hit, _meleeAttackLength, _masks))
            {
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                if(damagable != null)
                {
                    damagable.TakeDamage(_damage);
                    Debug.Log(hit.collider.name);
                }
            }
            Debug.DrawRay(_meleePoint.position, _meleePoint.forward * _meleeAttackLength);
        }
    }
}
