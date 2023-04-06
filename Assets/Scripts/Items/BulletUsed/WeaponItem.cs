using System;
using UnityEngine;

/// <summary>
///     Класс оружий
/// </summary>
public class WeaponItem : BulletUsedItem
{
    private int _damage;

    public WeaponItem(string name, int id, int count, int stackSize, float weight, int damage, BulletType type,
        Sprite image)
    {
        Id = id;
        Count = count;
        StackSize = stackSize;
        Weight = weight;
        Name = name;
        ItemType = ItemType.Weapon;
        Damage = damage;
        BulletType = type;
        Image = image;
    }

    public int Damage
    {
        get => _damage;
        set => _damage = Math.Max(0, value);
    }
}