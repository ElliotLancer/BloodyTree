using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using static UnityEditor.Progress;
using NUnit.Framework.Constraints;
public class Inventory : MonoBehaviour
{
    [SerializeField] private Image[] _inventorySlots;
    [SerializeField] private Color _defaultBackgroundColor;
    [SerializeField] private ItemHandler _itemHandler;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private ItemUI _itemUI;

    private GameObject[] _items = new GameObject[5];
    private int _currentSlotIndex = 0;
    private bool _currentSlotHasItem;
    public bool CurrentSlotHasItem => _currentSlotHasItem;
    public void Start()
    {
        _currentSlotHasItem = _items[_currentSlotIndex] != null;
        ChangeSlotColor();
    }
    private void OnEnable()
    {
        _playerHealth.OnPlayerDeath += RemoveAllItems;
    }
    private void OnDisable()
    {
        _playerHealth.OnPlayerDeath -= RemoveAllItems;
    }
    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCurrentSlot();
        }

    }
    public void AddItem(GameObject item)
    {
        Item itemToAdd = item.GetComponent<Item>();

        if (itemToAdd == null)
            return;

        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                GameObject addedItem = _items[i] = itemToAdd.gameObject;
                int indexOfAddedItem = Array.IndexOf(_items, addedItem);
                if (_currentSlotHasItem)
                {
                    _items[indexOfAddedItem].SetActive(false);
                }
                break;
            }
            else
            {
                continue;
            }
        }

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].sprite == null)
            {
                _inventorySlots[i].sprite = itemToAdd.IconImage;
                break;
            }
            else
            {
                continue;
            }
        }
        _currentSlotHasItem = _items[_currentSlotIndex] != null;

    }
    public void RemoveItem()
    {
        if (_items[_currentSlotIndex] == null)
        {
            Debug.Log("There is no item selected");
            return;
        }

        Debug.Log(_items[_currentSlotIndex].name + " removed from Inventory");
        _inventorySlots[_currentSlotIndex].sprite = null;
        _items[_currentSlotIndex].SetActive(true);
        _items[_currentSlotIndex] = null;

        _currentSlotHasItem = _items[_currentSlotIndex] != null;
    }
    public void RemoveAllItems()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _inventorySlots[i].sprite = null;
            _items[_currentSlotIndex].SetActive(true);
            _items[_currentSlotIndex] = null;
        }
    }
    public void ChangeCurrentSlot()
    {
        
        if (_items[_currentSlotIndex] != null)
            _items[_currentSlotIndex].SetActive(false);

        _currentSlotIndex++;

        if (_currentSlotIndex >= _inventorySlots.Length)
        {
            _currentSlotIndex = 0;
        }

        if (_items[_currentSlotIndex] != null)
            _items[_currentSlotIndex].SetActive(true);

        _currentSlotHasItem = _items[_currentSlotIndex] != null;
        _itemHandler.UpdateItem(_items[_currentSlotIndex]);
        ChangeSlotColor();
        _itemUI.UpdateUIPanel();
    }
    public void ChangeSlotColor()
    {

        Transform slotbackGround = _inventorySlots[_currentSlotIndex].transform.parent;
        int index = _currentSlotIndex == 0 ? _inventorySlots.Length - 1 : _currentSlotIndex - 1;

        Transform previousSlotbackGround = _inventorySlots[index].transform.parent;
        RawImage backgroundColor = previousSlotbackGround.GetComponent<RawImage>();
        backgroundColor.color = _defaultBackgroundColor;

        RawImage color = slotbackGround.GetComponent<RawImage>();
        color.color = Color.yellow;
    }
    public GameObject CheckItemInCurrentSlot()
    {
        return _items[_currentSlotIndex];
    }
    public void CheckCurrentSlot()
    {
        if (_items[_currentSlotIndex] == null)
        {
            for(int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    _items[i].SetActive(false);
                }
                else
                {
                    continue;
                }
            }
        }
    }
    public bool CheckIfInventoryFull()
    {
        bool isNotFull = false;

        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                isNotFull = true; 
            }
        }
        return !isNotFull;
    }
}
