using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isStoreSlot = true;
    public bool isFirstInventorySlot = false;

    private Image image;
    private RectTransform rectTransform;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DraggableUI draggedItem = eventData.pointerDrag.GetComponent<DraggableUI>();

            if (isStoreSlot)
            {
                if (draggedItem.isStoreSlot)
                {
                    return;
                }
                else
                {
                    HandleRefund(draggedItem);
                }
            }
            else
            {
                HandleItemSwap(draggedItem);

                if (isFirstInventorySlot)
                {
                    Debug.Log("Equipping weapon from the first slot.");
                    playerInventory.EquipWeaponFromFirstSlot();
                }
            }
        }
    }

    private void HandleRefund(DraggableUI draggedItem)
    {
        draggedItem.previousParent = draggedItem.transform.parent;
        draggedItem.transform.SetParent(transform);
        draggedItem.rectTransform.position = rectTransform.position;

        draggedItem.isStoreSlot = true;

        playerInventory.RemoveWeaponModel();

        Debug.Log("Item refunded: " + draggedItem.name);
    }

    private void HandleItemSwap(DraggableUI draggedItem)
    {
        if (IsSlotEmpty())
        {
            draggedItem.previousParent = draggedItem.transform.parent;
            draggedItem.transform.SetParent(transform);
            draggedItem.rectTransform.position = rectTransform.position;
        }
        else
        {
            DraggableUI currentItem = GetComponentInChildren<DraggableUI>();

            Transform currentParent = currentItem.transform.parent;
            currentItem.transform.SetParent(draggedItem.previousParent);
            currentItem.rectTransform.position = draggedItem.previousParent.GetComponent<RectTransform>().position;

            draggedItem.previousParent = draggedItem.transform.parent;
            draggedItem.transform.SetParent(currentParent);
            draggedItem.rectTransform.position = rectTransform.position;
        }

        draggedItem.isStoreSlot = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }

    private bool IsSlotEmpty()
    {
        return transform.childCount == 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GameManager.isStore)
            {
                DraggableUI rightClickedItem = GetComponent<DraggableUI>();
            }
        }
    }
}
