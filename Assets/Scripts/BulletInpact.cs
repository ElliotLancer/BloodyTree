using UnityEngine;

public class BulletInpact : MonoBehaviour
{
    [SerializeField] private float _disappearTime;
    private void Start()
    {
        Destroy(gameObject, _disappearTime);
    }
}
