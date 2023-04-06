using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
///     Класс для контроля UI-элементов слота инвентаря.
/// </summary>
public class ItemSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject lockObject;
    [SerializeField] public Image itemImage;
    [SerializeField] public TextMeshProUGUI itemCountText;
    [SerializeField] public TextMeshProUGUI unlockCostText;

    private string _cost;
    private InventoryController _inventoryController;
    private bool _isUnlocked;
    public int Id { get; set; }

    public string Cost
    {
        get => _cost;
        set
        {
            _cost = value;
            UpdateCost();
        }
    }

    private void Start()
    {
        _inventoryController = FindObjectOfType<InventoryController>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !_isUnlocked || !eventData.pointerDrag.GetComponent<DragAndDrop>()) return;

        eventData.pointerDrag.GetComponent<DragAndDrop>().DragEnding();
        _inventoryController.SwapSlots(Id,
            eventData.pointerDrag.GetComponent<DragAndDrop>().CurrentParent.GetComponent<ItemSlotUI>().Id);
    }

    private void UpdateCost()
    {
        unlockCostText.text = Cost;
    }

    public void UpdateInfo(Sprite itemSprite = null, int itemCount = -1)
    {
        if (itemSprite != null && itemCount != -1)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = itemSprite;
            itemCountText.text = itemCount > 1 ? itemCount.ToString() : "";

            return;
        }

        itemImage.gameObject.SetActive(false);
    }

    public void Unlock()
    {
        _isUnlocked = true;
        lockObject.SetActive(false);
    }
}