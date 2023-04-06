using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     База данных. Хранит информацию о типовых предметах, которые могут быть в инвентаре.
/// </summary>
[Serializable]
public class DataBase : MonoBehaviour
{
    [SerializeField] public List<ItemPattern> items;

    private void Awake()
    {
        for (var i = 0; i < items.Count; i++) items[i].ID = i;
    }

    public Sprite GetImageById(int id)
    {
        return items[id].image;
    }
}