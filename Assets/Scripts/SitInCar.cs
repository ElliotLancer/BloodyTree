using UnityEngine;

public class SitInCar : MonoBehaviour
{
    [SerializeField] private CarMovement drive;
    public void GetIntoCar(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(false);
        drive.SetCanDrive(true);
    }
    public void GetOutOfCar(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        drive.SetCanDrive(false);
    }
}
