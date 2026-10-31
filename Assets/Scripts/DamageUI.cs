using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
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
}
