public interface IInventory
{
    int AddItem(ItemData item, int amount);   // returns actually added
    int RemoveItem(ItemData item, int amount);
    int GetItemCount(ItemData item);
}