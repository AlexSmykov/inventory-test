using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
///     Класс-контроллер для управленя инвентарем
/// </summary>
public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject slotGameObject;
    [SerializeField] private PlayerResources playerResources;
    [SerializeField] private ItemFabric itemFabric;
    [SerializeField] private DataBase dataBase;
    
    [SerializeField] private int freeSlotCount;
    [SerializeField] private List<int> lockedSlotCosts;
    private string _savePath;

    // Изначально, все слоты инвентаря заблокированы, но при старте разблокируется необходимо кол-во
    private readonly Inventory _inventory = new();

    private int _currentBuyAvailable;
    private int _currentInventorySize;

    private ItemSlotUI[] _slots;

    private void Start()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "data.json");
#if UNITY_EDITOR
        _savePath = "Assets/Resources/data.json";
#endif
        
        // Если это наш первый запуск, то загружать не чего, задаем стартовые значения
        var isFirstSetup = bool.Parse(PlayerPrefs.GetString("IsFirstSetup", "true"));

        _slots = slotGameObject.GetComponentsInChildren<ItemSlotUI>(true);
        _slots[0].Cost = lockedSlotCosts[Math.Min(_currentBuyAvailable, lockedSlotCosts.Count - 1)] + "$";
        for (var i = 0; i < _slots.Length; i++) _slots[i].Id = i;

        _currentBuyAvailable = 0;
        _currentInventorySize = 0;

        if (isFirstSetup)
        {
            PlayerPrefs.SetString("IsFirstSetup", "false");

            for (var i = 0; i < freeSlotCount; i++) UnlockNewSlot();
        }
        else
        {
            Load();
        }
    }

    public void OnShot()
    {
        var isSuccessful = _inventory.OnBulletShot();

        if (!isSuccessful) Debug.Log("У вас нет патронов для стрельбы!");

        UpdateUiSlots();
    }

    public void OnAddBullets()
    {
        _inventory.AddItem(itemFabric.CreateBulletFull(BulletType.Bullet1));
        _inventory.AddItem(itemFabric.CreateBulletFull(BulletType.Bullet2));

        UpdateUiSlots();
    }

    public void OnAddItem()
    {
        _inventory.AddItem(itemFabric.Create(ItemType.Body, 1));
        _inventory.AddItem(itemFabric.Create(ItemType.Head, 1));
        _inventory.AddItem(itemFabric.Create(ItemType.Weapon, 1));

        UpdateUiSlots();
    }

    public void OnDeleteItem()
    {
        var isSuccessful = _inventory.RemoveRandom();

        if (!isSuccessful) Debug.Log("У вас нет предметов для удаления!");

        UpdateUiSlots();
    }

    /// <summary>
    ///     Метод для свапа значений в ячейке (DragAndDrop) предмета на свободную ячейку
    /// </summary>
    /// <param name="id1">Номер первого слота</param>
    /// <param name="id2">Номер второго слота</param>
    public void SwapSlots(int id1, int id2)
    {
        _inventory.Swap(id1, id2);

        UpdateUiSlots();
    }

    /// <summary>
    ///     Метод обновления UI предметов по данным с инвентаря
    /// </summary>
    private void UpdateUiSlots()
    {
        var info = _inventory.GetSlotInfo();

        for (var i = 0; i < info.Count; i++)
        {
            if (info[i] == "")
            {
                _slots[i].UpdateInfo();
                continue;
            }

            var data = info[i].Split(";");
            _slots[i].UpdateInfo(dataBase.GetImageById(int.Parse(data[0])), int.Parse(data[1]));
        }
        
        Save();
    }

    public void OnBuyTap()
    {
        if (playerResources.Money < lockedSlotCosts[Math.Min(_currentBuyAvailable, lockedSlotCosts.Count - 1)]) return;

        playerResources.Money -= lockedSlotCosts[Math.Min(_currentBuyAvailable, lockedSlotCosts.Count - 1)];
        _currentBuyAvailable++;
        
        UnlockNewSlot();
        Save();
    }

    private void UnlockNewSlot()
    {
        _inventory.Slots.Add(null);
        _slots[_currentInventorySize].Unlock();
        _currentInventorySize++;

        // Список цен может быть меньше чем кол-во заблокированных ячеек. Тогда используется последняя цена
        _slots[_currentInventorySize].Cost =
            lockedSlotCosts[Math.Min(_currentBuyAvailable, lockedSlotCosts.Count - 1)] + "$";
    }

    /// <summary>
    ///     Сохранение происходит в Json файл. Сохраняется кол-во разблокированных ячеек и список предметов инвентаря
    /// </summary>
    private void Save()
    {
        var json = JsonUtility.ToJson((_currentBuyAvailable, _inventory.GetSlotInfo()));
        File.WriteAllText(_savePath, json);
    }

    /// <summary>
    ///     Загружаем данные из Json файла
    /// </summary>
    private void Load()
    {
        var info = JsonUtility.FromJson<(int, List<string>)>(File.ReadAllText(_savePath));
        _currentBuyAvailable = info.Item1;

        for (var i = 0; i < info.Item2.Count; i++)
        {
            UnlockNewSlot();

            if (info.Item2[i] == "") continue;

            var data = info.Item2[i].Split(";");
            _inventory.Slots[i] = itemFabric.CreateById(int.Parse(data[0]), int.Parse(data[1]));
        }

        UpdateUiSlots();
    }
}