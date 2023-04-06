using System.Linq;
using UnityEngine;

/// <summary>
///     Фабрика для создания экземпляров предметов инвентаря
/// </summary>
public class ItemFabric : MonoBehaviour
{
    [SerializeField] private DataBase dataBase;

    public InventoryItem Create(ItemType type, int count = -1)
    {
        var availableParams = dataBase.items.Where(item => item.type == type).ToList();
        var chosenParams = availableParams[Random.Range(0, availableParams.Count)];
        var itemCount = count == -1 ? chosenParams.stackSize : count;
        return type switch
        {
            ItemType.Body => new BodyItem(chosenParams.name, chosenParams.ID, itemCount, chosenParams.stackSize,
                chosenParams.weight,
                ((ArmorPattern)chosenParams).defence, chosenParams.image),
            ItemType.Head => new HeadItem(chosenParams.name, chosenParams.ID, itemCount, chosenParams.stackSize,
                chosenParams.weight,
                ((ArmorPattern)chosenParams).defence, chosenParams.image),
            ItemType.Weapon => new WeaponItem(chosenParams.name, chosenParams.ID, itemCount, chosenParams.stackSize,
                chosenParams.weight,
                ((WeaponPattern)chosenParams).damage, ((WeaponPattern)chosenParams).bulletType, chosenParams.image),
            _ => new BulletItem(chosenParams.name, chosenParams.ID, itemCount, chosenParams.stackSize,
                chosenParams.weight, ((BulletPattern)chosenParams).bulletType, chosenParams.image)
        };
    }

    /// <summary>
    ///     Когда нужно добавить полные стаки патронов
    /// </summary>
    /// <param name="type"> Тип патронов, которые нужно добавить</param>
    /// <returns>Возвращает полный стак патронов нужного типа</returns>
    public InventoryItem CreateBulletFull(BulletType type)
    {
        var availableParams = dataBase.items.Where(item => item.type == ItemType.Bullet &&
                                                           ((BulletPattern)item).bulletType == type).ToList();
        var chosenParams = availableParams[Random.Range(0, availableParams.Count)];

        return new BulletItem(chosenParams.name, chosenParams.ID, chosenParams.stackSize, chosenParams.stackSize,
            chosenParams.weight, type, chosenParams.image);
    }

    /// <summary>
    ///     При загрузке предметов, мы может создать их по айди и задать им кол-во экземпляров
    /// </summary>
    /// <param name="id">Айди для выбора нужного предмета</param>
    /// <param name="itemCount">Количество предметов в стаке</param>
    /// <returns>Возвращает нужный предмет с нужным кол-вом экземпляров в стаке</returns>
    public InventoryItem CreateById(int id, int itemCount)
    {
        var chosenParams = dataBase.items[id];
        return chosenParams.type switch
        {
            ItemType.Body => new BodyItem(chosenParams.name, id, itemCount, chosenParams.stackSize, chosenParams.weight,
                ((ArmorPattern)chosenParams).defence, chosenParams.image),
            ItemType.Head => new HeadItem(chosenParams.name, id, itemCount, chosenParams.stackSize, chosenParams.weight,
                ((ArmorPattern)chosenParams).defence, chosenParams.image),
            ItemType.Weapon => new WeaponItem(chosenParams.name, id, itemCount, chosenParams.stackSize,
                chosenParams.weight,
                ((WeaponPattern)chosenParams).damage, ((WeaponPattern)chosenParams).bulletType, chosenParams.image),
            _ => new BulletItem(chosenParams.name, id, itemCount, chosenParams.stackSize,
                chosenParams.weight, ((BulletPattern)chosenParams).bulletType, chosenParams.image)
        };
    }
}