using UnityEngine;

/// <summary>
///     Класс предметов для защиты головы
/// </summary>
public class HeadItem : ArmorItem
{
    public HeadItem(string name, int id, int count, int stackSize, float weight, int defence, Sprite image)
    {
        Id = id;
        Count = count;
        StackSize = stackSize;
        Weight = weight;
        Name = name;
        Defence = defence;
        ItemType = ItemType.Head;
        Image = image;
    }
}