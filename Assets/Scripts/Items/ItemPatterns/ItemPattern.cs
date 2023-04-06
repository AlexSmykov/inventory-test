using System;
using UnityEngine;

/// <summary>
///     Класс-паттерн, который хранит в себе параметры, по которым будут создаваться объекты в фабрике
/// </summary>
public class ItemPattern : MonoBehaviour
{
    [SerializeField] public Sprite image;
    [SerializeField] public ItemType type;
    [SerializeField] public int stackSize;
    [SerializeField] public float weight;

    [NonSerialized] public int ID;
}