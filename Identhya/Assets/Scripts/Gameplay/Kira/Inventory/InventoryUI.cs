using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityStandardAssets.CrossPlatformInput;

public class InventoryUI : MonoBehaviour {
    /*
    public Transform itemsParent;
    public GameObject inventoryUI;
    Inventory inventory;
    public List<InventorySlot> slotsPool;
    public InventorySlot SlotPrefab;
    //[SerializeField] InspectionModeManager m_InspectManager;
    public GameObject giveButton;
    public GameObject backButton;
    public bool CanOfferFood = false;


    void Start() {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        if (slotsPool == null) {
            slotsPool = new List<InventorySlot>();
        }
        if (slotsPool.Count <= 0) {
            slotsPool.AddRange(itemsParent.GetComponentsInChildren<InventorySlot>());
        }
    }

    void UpdateUI() {
        while (inventory.items.Count > slotsPool.Count) {
            var newSlot = Instantiate(SlotPrefab,itemsParent);
           
            //newSlot.GetComponent<InventorySlot>().m_InspectManager = m_InspectManager; 
            slotsPool.Add(newSlot);

            //MenusManager.instance.SetObjectCollected(newSlot.gameObject);
        }
        for (int i = 0; i < slotsPool.Count; i++) {
            if (i < inventory.items.Count) {
                //slotsPool[i].AddItem(inventory.items[i]);
            } else {
                //slotsPool[i].ClearSlot();
            }
        }
    }

    public void EnableSlotButton()
    {
        foreach (var slot in slotsPool)
        {
            slot.GetComponent<Button>().interactable = true;
        }
    }

    public void DisableSlotButton()
    {
        foreach (var slot in slotsPool)
        {
            slot.GetComponent<Button>().interactable = false;
        }
    }*/
}
