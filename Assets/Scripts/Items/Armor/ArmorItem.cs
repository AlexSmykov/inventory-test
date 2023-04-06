using System;

/// <summary>
///     Класс предметов, которые являются бронёй
/// </summary>
public abstract class ArmorItem : InventoryItem
{
    private int _defence;

    public int Defence
    {
        get => _defence;
        set => _defence = Math.Max(0, value);
    }
}