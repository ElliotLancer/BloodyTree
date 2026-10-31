using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform _camera;
    [SerializeField] private float mouseSensitivity = 200f;
    private float _xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float moveMouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= moveMouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // Камера вверх/вниз
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // Тело влево/вправо
        playerBody.Rotate(Vector3.up * moveMouseX);
    }
}
