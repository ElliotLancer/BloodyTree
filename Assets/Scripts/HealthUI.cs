using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private Image _image;
    private void OnEnable()
    {
        _health.OnHealthChanged += ChangeHealthUI;
    }
    private void OnDisable()
    {
        _health.OnHealthChanged -= ChangeHealthUI;
    }
    private void ChangeHealthUI()
    {
        _image.fillAmount = _health.Health / _health.MaxHealth;
    }
}
