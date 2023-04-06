using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Класс инвентаря. Содержит в себе слоты с предметами и может взаимодействовать с ними
/// </summary>
public class Inventory
{
    public readonly List<InventoryItem> Slots;

    public Inventory()
    {
        Slots = new List<InventoryItem>();
    }

    public bool AddItem(InventoryItem itemToAdd)
    {
        foreach (var slot in Slots.Where(slot => slot != null && slot.Id == itemToAdd.Id))
        {
            itemToAdd = slot.AddCopies(itemToAdd);

            if (itemToAdd == null) return true;
        }

        for (var i = 0; i < Slots.Count; i++)
        {
            if (Slots[i] != null) continue;

            Slots[i] = itemToAdd;
            return true;
        }

        return false;
    }

    public bool OnBulletShot()
    {
        var availableItems = Slots.Where(slot =>
            slot is BulletItem item && !item.IsStackFull()).ToList();

        if (availableItems.Count == 0) availableItems = Slots.Where(slot => slot is BulletItem).ToList();

        if (availableItems.Count == 0) return false;

        ((BulletItem)availableItems[Random.Range(0, availableItems.Count)]).OnShot();
        CheckCounts();

        return true;
    }

    public bool RemoveRandom()
    {
        var availableIds = new List<int>();
        
        for (var i = 0; i < Slots.Count; i++)
        {
            if (Slots[i] != null)
            {
                availableIds.Add(i);
            }
        }
        
        if (availableIds.Count == 0) return false;
        Slots[availableIds[Random.Range(0, availableIds.Count)]] = null;
        return true;
    }

    public List<string> GetSlotInfo()
    {
        return Slots.Select(slot => slot == null ? "" : slot.ToString()).ToList();
    }

    private void CheckCounts()
    {
        for (var i = 0; i < Slots.Count; i++)
            if (Slots[i] != null && Slots[i].Count == 0)
                Slots[i] = null;
    }

    public void Swap(int id1, int id2)
    {
        (Slots[id1], Slots[id2]) = (Slots[id2], Slots[id1]);
    }
}