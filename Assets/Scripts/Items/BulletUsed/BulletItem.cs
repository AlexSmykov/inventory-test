using UnityEngine;

/// <summary>
///     Класс патронов
/// </summary>
public class BulletItem : BulletUsedItem
{
    public BulletItem(string name, int id, int count, int stackSize, float weight, BulletType type, Sprite image)
    {
        Id = id;
        Count = count;
        StackSize = stackSize;
        Weight = weight;
        Name = name;
        ItemType = ItemType.Bullet;
        BulletType = type;
        Image = image;
    }

    public void OnShot()
    {
        _count--;
    }
}