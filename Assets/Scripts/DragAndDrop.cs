using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
///     Класс для предметов, которые могут перетаскиваться (все предметы инвентаря, этот метод будет висеть на них)
///     Перемещение предметов сделано следующим образом: при перемещении изображении объекта на свободную ячейку,
///     картинка возвращается на свое старое место, после чего в класс инвентаря посылается запрос на смену местами
///     двух мест (передаюстя айдишники этих мест)
/// </summary>
public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Material spriteMaterial;
    private Vector2 _anchoredPosition;

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Transform _inventoryObject;
    private RectTransform _rectTransform;
    [NonSerialized] public Transform CurrentParent;

    private void Awake()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _inventoryObject = GameObject.Find("Inventory").GetComponent<Transform>();

        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _anchoredPosition = _rectTransform.anchoredPosition;
        CurrentParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Меняем материал, чтобы изображение могло проходить сквозь ViewPort 
        GetComponent<Image>().material = spriteMaterial;

        // Ставим более высокого родителя, чтобы на момент перетаскивания изображение было выше остальных
        transform.parent = _inventoryObject;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEnding();
    }

    public void DragEnding()
    {
        GetComponent<Image>().material = null;
        transform.parent = CurrentParent;
        _canvasGroup.blocksRaycasts = true;
        _rectTransform.anchoredPosition = _anchoredPosition;
        _canvasGroup.alpha = 1;
    }
}