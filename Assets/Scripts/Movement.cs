using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float _moveSpeed = 3f;

    private void Update()
    {
        Moving();
    }
    protected virtual void Moving()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0f;

        transform.position += move * _moveSpeed * Time.deltaTime;

        float speed = new Vector2(moveX, moveZ).magnitude;
        animator.SetFloat("Speed", speed);
    }
}
