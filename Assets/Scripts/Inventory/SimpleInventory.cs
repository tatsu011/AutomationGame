using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimpleInventory : IInventory
{
    [Serializable]
    public class Stack
    {
        public ItemData item;
        public int amount;
    }

    public int maxStacks = 16;
    public List<Stack> stacks = new();

    public int AddItem(ItemData item, int amount)
    {
        if (item == null || amount <= 0) return 0;

        var stack = stacks.Find(s => s.item == item);
        if (stack == null)
        {
            if (stacks.Count >= maxStacks) return 0;
            stack = new Stack { item = item, amount = 0 };
            stacks.Add(stack);
        }

        stack.amount += amount;
        return amount;
    }

    public int RemoveItem(ItemData item, int amount)
    {
        if (item == null || amount <= 0) return 0;
        var stack = stacks.Find(s => s.item == item);
        if (stack == null) return 0;

        int removed = Mathf.Min(amount, stack.amount);
        stack.amount -= removed;
        if (stack.amount <= 0) stacks.Remove(stack);
        return removed;
    }

    public int GetItemCount(ItemData item)
    {
        var stack = stacks.Find(s => s.item == item);
        return stack?.amount ?? 0;
    }
}