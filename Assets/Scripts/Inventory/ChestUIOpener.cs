using UnityEngine;

[RequireComponent(typeof(InventoryComponent))]
public class ChestUIOpener : MonoBehaviour
{
    private InventoryComponent _inv;

    void Awake()
    {
        _inv = GetComponent<InventoryComponent>();
    }

    void OnMouseDown()
    {
        if (InventoryUIManager.Instance != null)
        {
            InventoryUIManager.Instance.Show(_inv);
        }
    }
}