using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory/Equipment Item")]
public class EquipmentItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject weaponModel;
}
