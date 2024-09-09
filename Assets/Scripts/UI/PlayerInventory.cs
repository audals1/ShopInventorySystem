using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Transform weaponSlot;
    public Transform firstInventorySlot;

    public DraggableUI equippedWeaponInstance;

    void Start()
    {
        EquipWeaponFromFirstSlot();
    }

    private void Update()
    {
        if (firstInventorySlot.transform.childCount == 0)
        {
            EquipWeaponFromFirstSlot();
        }
    }

    public void EquipWeaponFromFirstSlot()
    {
        DraggableUI firstSlotWeapon = firstInventorySlot.GetComponentInChildren<DraggableUI>();

        if (firstSlotWeapon != null)
        {
            EquipWeapon(firstSlotWeapon);
        }
        else
        {
            if (equippedWeaponInstance != null)
            {
                equippedWeaponInstance.equipmentItem.weaponModel.SetActive(false);
                Destroy(equippedWeaponInstance.gameObject);
                equippedWeaponInstance = null;
                RemoveWeaponModel();
            }
        }
    }

    private void EquipWeapon(DraggableUI newWeaponPrefab)
    {
        if (equippedWeaponInstance != null)
        {
            RemoveWeaponModel();
        }

        equippedWeaponInstance = Instantiate(newWeaponPrefab, weaponSlot);
        equippedWeaponInstance.transform.SetParent(weaponSlot);
        equippedWeaponInstance.transform.position = weaponSlot.position;

        SetWeaponModel(equippedWeaponInstance.equipmentItem);

    }

    private void SetWeaponModel(EquipmentItem item)
    {
        GameObject newWeaponModel = Instantiate(item.weaponModel, weaponSlot);
        newWeaponModel.SetActive(true);
        newWeaponModel.transform.localPosition = Vector3.zero;
        newWeaponModel.transform.localRotation = Quaternion.identity;
    }

    public void RemoveWeaponModel()
    {
        foreach (Transform child in weaponSlot)
        {
            Equipment equipment = child.GetComponent<Equipment>();
            DraggableUI draggableUI = child.GetComponent<DraggableUI>();
            if (equipment != null)
            {
                Destroy(equipment.gameObject);
            }
            if (draggableUI != null)
            {
                Destroy(draggableUI.gameObject);
            }
        }
    }

}