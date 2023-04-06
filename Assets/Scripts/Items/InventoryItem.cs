using System;
using UnityEngine;

/// <summary>
///     Класс предмета инвентаря
/// </summary>
public abstract class InventoryItem
{
    protected int _count;
    public int Id { get; set; }

    public Sprite Image { get; set; }
    public ItemType ItemType { get; set; }
    public string Name { get; set; }

    public int Count
    {
        get => _count;
        set => _count = Math.Max(0, value);
    }

    public int StackSize { get; set; }

    public float Weight { get; set; }

    public InventoryItem AddCopies(InventoryItem itemToAdd)
    {
        var availableSpace = StackSize - Count;

        if (availableSpace < itemToAdd.Count)
        {
            _count = StackSize;
            itemToAdd.Count -= availableSpace;
            return itemToAdd;
        }

        _count += itemToAdd.Count;
        return null;
    }

    public bool IsStackFull()
    {
        return Count == StackSize;
    }

    public override string ToString()
    {
        return Id + ";" + Count;
    }
}