using UnityEngine;

public class ConveyorBuilding : Building
{
    public SimpleInventory buffer = new();
    public float moveInterval = 0.3f;
    private float _timer;
    public InventoryComponent inventoryComponent;

    void Awake()
    {
        if (inventoryComponent == null)
            inventoryComponent = GetComponent<InventoryComponent>();
    }

    public override void Tick(float deltaTime)
    {
        _timer += deltaTime;
        if (_timer < moveInterval) return;
        _timer = 0f;

        if (buffer.stacks.Count == 0) return;

        // Take the first stack
        var stack = buffer.stacks[0];
        if (stack.amount <= 0) return;

        // Find building ahead
        Vector2Int targetPos = GridPosition + Facing.ToVector2Int();
        var targetBld = GridManager.Instance.GetBuilding(targetPos);
        if (targetBld == null) return;

        var targetInvComp = targetBld.GetComponent<InventoryComponent>();
        if (targetInvComp == null) return;

        // Move 1 item
        int moved = targetInvComp.Inventory.AddItem(stack.item, 1);
        if (moved > 0)
            buffer.RemoveItem(stack.item, moved);
    }
}