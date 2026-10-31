using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _camPosWhenAlive;
    [SerializeField] private Transform _camPosWhenDead;
    [SerializeField] private Transform _cam;
    private void Start()
    {
        _cam.SetParent(_camPosWhenAlive);
    }
    private void OnDisable()
    {
        SwitchCameraPosition();
    }
    public void SwitchCameraPosition()
    {
        _cam.SetParent(_camPosWhenDead);
    }
}
