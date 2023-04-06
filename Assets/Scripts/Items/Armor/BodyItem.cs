using UnityEngine;

/// <summary>
///     Класс предметов для защиты торса
/// </summary>
public class BodyItem : ArmorItem
{
    public BodyItem(string name, int id, int count, int stackSize, float weight, int defence, Sprite image)
    {
        Id = id;
        Count = count;
        StackSize = stackSize;
        Weight = weight;
        Name = name;
        Defence = defence;
        ItemType = ItemType.Body;
        Image = image;
    }
}