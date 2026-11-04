using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _standingOffset;
    [SerializeField] private float _crouchingOffset;

    [SerializeField] private Transform _crouchCheckPoint;
    [SerializeField] private float _crouchRaycastLength;

    [SerializeField] private Vector3 _crouchCenterOffset;
    [SerializeField] private Vector3 _standingCenterOffset;

    private bool _isCrouching;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isCrouching = false;
        }
        if (!_isCrouching)
        {
            CheckForObstacleAbove();
        }
    }
    private void StandUp()
    {
        _collider.height = _standingOffset;
        _collider.center = _standingCenterOffset;
        _movement.SetCrouchSpeed(false);
    }
    private void Crouch()
    {
        _collider.height = _crouchingOffset;
        _collider.center = _crouchCenterOffset;
        _movement.SetCrouchSpeed(true);
        _isCrouching = true;
    }
    private void CheckForObstacleAbove()
    {
        bool HasObsticleAbove = Physics.Raycast(_crouchCheckPoint.position, Vector3.up, _crouchRaycastLength);
        if (!HasObsticleAbove)
        {
            StandUp();
        }
    }
}
