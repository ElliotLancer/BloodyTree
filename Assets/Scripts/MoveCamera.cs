using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _camPosWhenAlive;
    [SerializeField] private Transform _camPosWhenDead;
    [SerializeField] private Transform _cam;
    private Car _currentCar;
    private void Start()
    {
        _cam.SetParent(_camPosWhenAlive);
    }
    private void OnDisable()
    {
        if (_currentCar != null && _currentCar.IsInCar)
            return;

        SwitchCameraPosition();
    }
    public void SwitchCameraPosition()
    {
        _cam.SetParent(_camPosWhenDead);
    }
    public void SetCurrentCar(Car car)
    {
        _currentCar = car;
    }
}
