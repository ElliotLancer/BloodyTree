using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private RaycastHitShot _raycast;

    [SerializeField] private float _fireRate;
    [SerializeField] private int _magazine;
    [SerializeField] private int _ammo;
    public int Ammo => _ammo;
    public int Magazine => _magazine;

    public event Action OnAmmoChanged;

    private bool _canShoot;

    private float _fireTimer;
    private void Start()
    {
        _canShoot = _ammo <= 0 ? false : true;
    }
    private void Update()
    {
        _fireTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && _fireTimer <= 0f && _canShoot)
        {
            Shoot();
            DecreaseAmmo();
            _fireTimer = _fireRate;
        }
    }
    public void Shoot()
    {
        _raycast.ShootRay();
    }
    private void DecreaseAmmo()
    {
        _ammo--;
        if (_ammo <= 0)
        {
            _canShoot = false;
        }
        OnAmmoChanged?.Invoke();
    }
}
