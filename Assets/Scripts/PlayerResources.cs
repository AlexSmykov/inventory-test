using System;
using TMPro;
using UnityEngine;

/// <summary>
///     Тут типа хранятся ресурсы игрока, в данном случае - деньги
/// </summary>
public class PlayerResources : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI MoneyText;
    [SerializeField] private int money;

    public int Money
    {
        get => money;
        set
        {
            money = Math.Max(0, value);
            UpdateText();
        }
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        MoneyText.text = Money + "$";
    }
}