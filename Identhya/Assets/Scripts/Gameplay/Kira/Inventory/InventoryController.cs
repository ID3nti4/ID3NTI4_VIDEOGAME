using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<InventoryItems> ForceItems;
    List<InventoryItems> InventoryList;

    public enum InventoryItems { Gloves, Boots, DolphinBone, Coin, FoodCan, Staff, Boomerang };

    private void Awake()
    {
        InventoryList = new List<InventoryItems>();
        foreach(InventoryItems item in ForceItems)
        {
            InventoryList.Add(item);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public bool HasItem(InventoryItems item)
    {
        return InventoryList.Contains(item);
    }

    public void GetItem(InventoryItems item)
    {
        InventoryList.Add(item);
        if (item == InventoryItems.Gloves) GetGloves();
        if (item == InventoryItems.Boots) GetBoots();
    }

    public bool HasBoots()
    {
        return InventoryList.Contains(InventoryItems.Boots);
    }

    public bool HasGloves()
    {
        return InventoryList.Contains(InventoryItems.Gloves);
    }

    public void GetBoots()
    {
        FindObjectOfType<EquipmentController>().GetBoots();
    }

    public void GetGloves()
    {
        FindObjectOfType<EquipmentController>().GetGloves();
    }
}
