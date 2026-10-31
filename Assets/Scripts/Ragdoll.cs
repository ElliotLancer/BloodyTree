using Unity.VisualScripting;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rbs;
    [SerializeField] private MonoBehaviour[] _scripts;
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Collider _mainCollider;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        DisableRagdoll();
    }
    public void DisableRagdoll()
    {
        _mainCollider.enabled = true;

        foreach (Rigidbody rb in _rbs)
        {
            rb.isKinematic = true;
        }
        foreach (Collider col in _colliders)
        {
            col.enabled = false;
        }
    }
    public void EnableRagdoll()
    {
        _mainCollider.enabled = false;

        foreach (Rigidbody rb in _rbs)
        {
            rb.isKinematic = false;
        }
        foreach (MonoBehaviour script in _scripts)
        {
            script.enabled = false;
        }
        foreach (Collider col in _colliders)
        {
            col.enabled = true;
        }
        _animator.enabled = false;
    }
}
