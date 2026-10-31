using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image _itemSpriteUI;
    [SerializeField] private TMP_Text _ammoText;
    private Item _currentItem;
    private Weapon _weapon;

    private void Start()
    {
        UpdateUIPanel();
    }
    private void UpdateAmmoUI()
    {
        _ammoText.text = $"{_weapon.Ammo} / {_weapon.Magazine}";
    }
    public void SetItem(Item item)
    {
        _currentItem = item;

        if (item != null)
        {
            Weapon weapon = item.GetComponent<Weapon>();
            if (weapon != null)
            {
                if (_weapon == null)
                {
                    _weapon = weapon;
                    _weapon.OnAmmoChanged += UpdateAmmoUI;
                }
                else
                {
                    _weapon.OnAmmoChanged -= UpdateAmmoUI;
                    _weapon = weapon;
                    _weapon.OnAmmoChanged += UpdateAmmoUI;

                }
            }
        }
        UpdateUIPanel();
        
    }
    public void UpdateUIPanel()
    {
        if (_currentItem != null)
        {
            _weapon = _currentItem.GetComponent<Weapon>();
            if (_weapon != null)
            {
                _ammoText.enabled = true;
                UpdateAmmoUI();
            }
            else
            {
                _ammoText.enabled = false;
            }
            _itemSpriteUI.sprite = _currentItem.IconImage;
        }
        else
        {
            _itemSpriteUI.sprite = null;
            _ammoText.enabled = false;
        }
    }
}
