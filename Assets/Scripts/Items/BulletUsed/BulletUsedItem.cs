/// <summary>
///     Предметы, которые зависят от типа патронов
/// </summary>
public abstract class BulletUsedItem : InventoryItem
{
    public BulletType BulletType { get; set; }
}