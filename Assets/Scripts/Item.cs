using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _iconImage;

    [SerializeField] private Material _material;
    [SerializeField] private Color _originalColor;
    [SerializeField] private ItemHandler _itemhandler;
    [SerializeField] private Vector3 _itemPosition;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _collider;


    private bool _canPickUp;
    private bool _isInHand;

    public Sprite IconImage => _iconImage;
    public Vector3 ItemPosition => _itemPosition;
    public bool IsInHand => _isInHand;
    private void Awake()
    {
        _isInHand = transform.parent == _itemhandler.MeleePoint;
        if (!_isInHand)
        {
            Disactivate();
        }
    }
    private void Start()
    {
        _material.color = _originalColor;
    }
    
    private void OnMouseEnter()
    {
        if (_canPickUp)
        {
            Debug.Log("Aimed on item");
            _material.color = Color.yellow;
            _itemhandler.ReadyToPickUp(true, this);
        }
    }
    private void OnMouseExit()
    {
        if (_canPickUp)
        {
            _material.color = _originalColor;
            _itemhandler.ReadyToPickUp(false, this);
        }
    }
    public void CanPickUp(bool condition)
    {
        _canPickUp = condition;
        if (!condition)
            _material.color = _originalColor;
    }

    public void SetInHand(bool value)
    {
        _isInHand = value;
    }

    private void Disactivate()
    {
        _rb.isKinematic = false;
        _collider.isTrigger = false;
        _itemhandler.SetItemScripts(this.gameObject, false);
    }
}
