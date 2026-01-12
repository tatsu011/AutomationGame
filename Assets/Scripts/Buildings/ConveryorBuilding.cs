using UnityEngine;

[RequireComponent(typeof(InventoryComponent))]
public class ConveyorBuilding : Building
{
    public float moveInterval = 0.3f;
    private float _timer;

    private InventoryComponent _inventoryComp;
    private SimpleInventory MyInventory => _inventoryComp.inventory;

    void Awake()
    {
        _inventoryComp = GetComponent<InventoryComponent>();
    }

    public override void Tick(float deltaTime)
    {
        _timer += deltaTime;
        if (_timer < moveInterval) return;
        _timer = 0f;

        // 1) Push items forward
        PushForward();

        // 2) Pull items from behind
        PullFromBack();
    }

    void PushForward()
    {
        if (MyInventory.stacks.Count == 0) return;

        // Building in front
        Vector2Int frontPos = GridPosition + Facing.ToVector2Int();
        var frontBld = GridManager.Instance.GetBuilding(frontPos);
        if (frontBld == null) return;

        var frontInvComp = frontBld.GetComponent<InventoryComponent>();
        if (frontInvComp == null) return;

        var stack = MyInventory.stacks[0];
        if (stack.amount <= 0) return;

        int moved = frontInvComp.inventory.AddItem(stack.item, 1);
        if (moved > 0)
        {
            MyInventory.RemoveItem(stack.item, moved);
        }
    }

    void PullFromBack()
    {
        // Building behind the conveyor
        Vector2Int backPos = GridPosition - Facing.ToVector2Int();
        var backBld = GridManager.Instance.GetBuilding(backPos);
        if (backBld == null) return;

        var backInvComp = backBld.GetComponent<InventoryComponent>();
        if (backInvComp == null) return;

        // Try to pull one item from the back inventory
        if (backInvComp.inventory.TryTakeAnyItem(out ItemData pulledItem) && pulledItem != null)
        {
            int added = MyInventory.AddItem(pulledItem, 1);
            if (added == 0)
            {
                // If couldn't add (no space / max stacks), put it back
                backInvComp.inventory.AddItem(pulledItem, 1);
            }
        }
    }
}