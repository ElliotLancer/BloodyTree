using UnityEngine;

public class CarDrive : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float mouseSensitivity;
    private bool _canDrive;
    private void Update()
    {
        if (_canDrive)
        {
            float z = Input.GetAxis("Horizontal");
            float x = Input.GetAxis("Vertical");

            Vector3 direction = transform.right * x + transform.forward * -z;
            direction.y = 0f;
            transform.position += direction * _speed * Time.deltaTime;

            float moveMouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float moveMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            gameObject.transform.Rotate(Vector3.up * moveMouseX);
        }
    }
    public void SetCanDrive(bool value)
    {
        _canDrive = value;
    }
}
