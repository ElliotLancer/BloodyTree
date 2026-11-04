using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed = 6f;
    [SerializeField] private float _crouchSpeed = 3f;

    [SerializeField] private Animator _animator;
    private float _moveSpeed;
    private void Start()
    {
        _moveSpeed = _defaultSpeed;
    }
    private void Update()
    {
        Moving();
    }
    public void Moving()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0f;

        transform.position += move * _moveSpeed * Time.deltaTime;

        float speed = new Vector2(moveX, moveZ).magnitude;
        _animator.SetFloat("Speed", speed);
    }
    public void SetCrouchSpeed(bool predicate)
    {
        _moveSpeed = predicate ? _crouchSpeed : _defaultSpeed;
    }
}
