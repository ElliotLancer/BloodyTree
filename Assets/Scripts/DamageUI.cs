using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _player;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.up * _speed;
    }
    public void SetDamageText(int damage)
    {
        _damageText.text = $"- {damage}";
    }
    public void SetUIRotation(Vector3 pos)
    {
        Vector3 dir = pos - _player.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
