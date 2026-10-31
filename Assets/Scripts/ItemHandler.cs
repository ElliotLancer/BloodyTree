using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Transform _meleePoint;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemUI _itemUI;

    private Vector3 _itemPosition;
    private Rigidbody _rb;
    private Collider _collider;
    private GameObject _inHandObject;
    private GameObject _temporaryInHandObject;
    private Item _temporaryItem;
    private Item _inHandItem;
    private Transform _itemTransform;
    private bool _canPickUp;

    public Transform MeleePoint => _meleePoint;

    private void Awake()
    {
        if (MeleePoint.childCount != 0)
        {
            Transform inHandItem = _meleePoint.GetChild(0);
            _inHandObject = inHandItem.gameObject;
            _rb = inHandItem.gameObject.GetComponent<Rigidbody>();
            _collider = inHandItem.gameObject.GetComponent<Collider>();
        }
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.V) && _inHandItem != null && _inventory.CurrentSlotHasItem)
        {
            DropItem();
        }
        if (Input.GetMouseButtonDown(1) && _inHandItem != null && _inventory.CurrentSlotHasItem)
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.B) && _canPickUp)
        {
            PickUpItem();
        }
    }
    public void SetItemScripts(GameObject item, bool condition)
    {
        MonoBehaviour[] scripts = item.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = condition;
        }
    }
    public void UseItem()
    {
        IUsable usableItem = _inHandItem.GetComponent<IUsable>();
        if (usableItem == null)
            return;

        _rb.isKinematic = false;
        _collider.isTrigger = false;
        usableItem.Use();

        _inventory.RemoveItem();
        SetItemScripts(_inHandObject, false);
        _inHandItem.SetInHand(false);
        _inHandItem = null;
        _itemUI.SetItem(null);
    }
    public void DropItem()
    {
        _inHandObject.transform.SetParent(null);
        _rb.isKinematic = false;
        _collider.isTrigger = false;
        _inventory.RemoveItem();
        SetItemScripts(_inHandObject, false);
        _inHandItem.SetInHand(false);
        _inHandItem = null;
        _rb.AddForce(transform.forward, ForceMode.Impulse);
        _itemUI.SetItem(null);
    }
    public void PickUpItem()
    {
        if (_temporaryItem == null)
            return;

        if (_inventory.CheckIfInventoryFull())
        {
            Debug.Log("Inventory is full");
            return;
        }

        SetCurrentItem(_temporaryItem);
        _temporaryItem = null;
        _temporaryInHandObject = null;
        GameObject currentItem = _inventory.CheckItemInCurrentSlot();
        if (currentItem != null)
        {
            UpdateItem(currentItem.gameObject);
        }
        else
        {
            _inventory.CheckCurrentSlot();
        }
    }
    public void ReadyToPickUp(bool condition, Item item)
    {
        _canPickUp = condition;
        _temporaryInHandObject = item.gameObject;
        _temporaryItem = item;
    }
    public void UpdateItem(GameObject newItem)
    {
        if (newItem != null)
        {
            Item nextItemInSlot = newItem.GetComponent<Item>();
            _inHandItem = nextItemInSlot;
            _inHandObject = nextItemInSlot.gameObject;
            _rb = nextItemInSlot.gameObject.GetComponent<Rigidbody>();
            _collider = nextItemInSlot.gameObject.GetComponent<Collider>();
            _itemUI.SetItem(nextItemInSlot);
        }
        else
        {
            _itemUI.SetItem(null);
        }
    }
    private void SetCurrentItem(Item item)
    {
        _inHandItem = item;
        _inHandObject = item.gameObject;

        _rb = item.gameObject.GetComponent<Rigidbody>();
        _collider = item.gameObject.GetComponent<Collider>();
        _itemTransform = item.gameObject.GetComponent<Transform>();

        _inHandObject.transform.SetParent(_meleePoint);
        _rb.isKinematic = true;
        _collider.isTrigger = true;
        _itemTransform.localPosition = item.ItemPosition;
        _itemTransform.localRotation = Quaternion.identity;
        SetItemScripts(item.gameObject, true);
        _inventory.AddItem(item.gameObject);
        _inHandItem.SetInHand(true);
        _itemUI.SetItem(item);
    }
}
