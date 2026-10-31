using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                Debug.Log($"{item.name} item is in zone");
                item.CanPickUp(true);
            }
                
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                item.CanPickUp(false);
                Debug.Log("Changed Item Exit");
            }
        }
    }
}
