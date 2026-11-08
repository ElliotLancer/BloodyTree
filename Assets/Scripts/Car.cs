using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Transform _sitPoint;
    [SerializeField] private Cameras _cameras;
    [SerializeField] private Transform _GetOutPoint;
    private bool _canSitInCar;
    private bool _isDestroied;
    private bool _isInCar;
    private SitInCar _sit;
    public bool IsInCar => _isInCar;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            _canSitInCar = false;
            return;
        }
        _canSitInCar = true;
        _sit = other.GetComponent<SitInCar>();
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canSitInCar = false;
            return;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canSitInCar == true && !_isDestroied)
        {
            _sit.GetIntoCar(_sitPoint.position, transform);
            _cameras.ChangeCameraToCar();
            _isInCar = true;
        }
        if (Input.GetKeyDown(KeyCode.G) && !_isDestroied && _isInCar)
        {
            _sit.GetOutOfCar(_GetOutPoint.position);
            _cameras.ChangeCameraToPlayer();
            _isInCar = false;
        }
    }
    public void CarDestroied()
    {
        _isDestroied = true;
    }
}
