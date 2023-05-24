using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    /*public Item item;
    public TextMeshProUGUI itemName;
    public InspectionModeManager m_InspectManager;

    public void AddItem(Item newItem) {
        item = newItem;
        itemName.text = item.name;
    }

    public void ClearSlot() {
        item = null;
        itemName.text = null;
        Destroy(gameObject);
    }

    public void UseItem() {
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();

        if (inventoryUI.CanOfferFood && item.name == "Food Can")
        {
            Debug.Log("Can Give Food");
            inventoryUI.giveButton.SetActive(true);
            inventoryUI.backButton.SetActive(false);
            MenusManager.instance.SetFirstObject(inventoryUI.giveButton);
            inventoryUI.DisableSlotButton();
            m_InspectManager.InspectItem(item);
        }
        else
        {
            if (item == null) return;
            m_InspectManager.InspectItem(item);
        }
    }*/
}