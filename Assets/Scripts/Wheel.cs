using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelCollider _wheelCol;
    [SerializeField] private Transform _wheelMesh;
    [SerializeField] private bool _wheelTurn;

    private void Update()
    {
        if (_wheelTurn)
        {
            _wheelMesh.localEulerAngles = new Vector3(_wheelMesh.localEulerAngles.x, -(_wheelCol.steerAngle - _wheelMesh.localEulerAngles.z), _wheelMesh.localEulerAngles.z);
        }
        _wheelMesh.Rotate(_wheelCol.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
}
