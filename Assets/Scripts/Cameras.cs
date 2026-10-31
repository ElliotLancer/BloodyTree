using UnityEngine;

public class Cameras : MonoBehaviour
{
    [SerializeField] private Camera _carCamera;
    [SerializeField] private Camera _playerCamera;

    public void ChangeCameraToCar()
    {
        _carCamera.gameObject.SetActive(true);
        _playerCamera.gameObject.SetActive(false);
    }
    public void ChangeCameraToPlayer()
    {
        _carCamera.gameObject.SetActive(false);
        _playerCamera.gameObject.SetActive(true);
    }
}
