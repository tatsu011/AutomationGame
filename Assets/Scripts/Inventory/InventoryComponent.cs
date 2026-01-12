using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    public SimpleInventory inventory = new();

    public IInventory Inventory => inventory;
}