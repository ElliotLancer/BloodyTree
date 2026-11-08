using TMPro;
using UnityEngine;

public class SitInCar : MonoBehaviour
{
    [SerializeField] private CarMovement _drive;
    [SerializeField] private MonoBehaviour[] _scripts;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    public void GetIntoCar(Vector3 pos, Transform car)
    {
        transform.position = pos;
        transform.parent = car;
        gameObject.layer = LayerMask.NameToLayer("InVehicle");
        _animator.enabled = false;
        SetScripts(false);
        _drive.SetCanDrive(true);
    }
    public void GetOutOfCar(Vector3 pos)
    {
        transform.position = pos;
        transform.parent = null;
        gameObject.layer = LayerMask.NameToLayer("Player");
        _animator.enabled = true;
        SetScripts(true);
        _drive.SetCanDrive(false);
    }
    private void SetScripts(bool cond)
    {
        foreach (var script in _scripts)
        {
            script.enabled = cond;
        }
        _rb.isKinematic = !cond;
    }
}
