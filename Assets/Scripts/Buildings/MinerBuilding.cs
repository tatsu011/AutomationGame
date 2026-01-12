using UnityEngine;

public class MinerBuilding : Building
{
    public ItemData outputItem;
    public int outputAmount = 1;
    public float miningInterval = 1.0f;

    private float _timer;

    public override void Tick(float deltaTime)
    {
        _timer += deltaTime;
        if (_timer < miningInterval) return;
        _timer = 0f;

        // Target tile in front
        Vector2Int targetPos = GridPosition + Facing.ToVector2Int();
        var targetBld = GridManager.Instance.GetBuilding(targetPos);
        if (targetBld == null) return;

        var invComp = targetBld.GetComponent<InventoryComponent>();
        if (invComp == null) return;

        invComp.Inventory.AddItem(outputItem, outputAmount);
    }
}