using UnityEngine;
using System.Collections;
public class Grenate : MonoBehaviour, IUsable
{
    [SerializeField] private float _throwForce;
    [SerializeField] private float _explosionTime;
    [SerializeField] private Explosion _explosion;
    private Rigidbody _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("pressed G");

            StartCoroutine(ActivateGrenate());
        }
    }
    public void Use()
    {
        transform.SetParent(null);
        ThrowGrenate();
        StartCoroutine(ActivateGrenate());
    }
    public void ThrowGrenate()
    {
        _rb.AddForce(transform.forward * _throwForce, ForceMode.Impulse);
    }
    private IEnumerator ActivateGrenate()
    {
        yield return new WaitForSeconds(_explosionTime);
        _explosion.StartExplosion();
    }
}
