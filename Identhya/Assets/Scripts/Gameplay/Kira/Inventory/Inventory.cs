using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    #region Singleton
    public static Inventory instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    [SerializeField] int maxSpace = 8;
    //public List<Item> items = new List<Item>();

    //public Item DefaultItem;

    /*public void Add(Item item) {
        if (!item.defaultItem) {
            if (items.Count >= maxSpace) {
                Debug.Log("Not enough space");
                return;
            }

            items.Add(item);

            if (OnItemChangedCallback != null) {
                OnItemChangedCallback.Invoke();
            }
        }
    }

    public void Remove(Item item) {
        items.Remove(item);
        if (OnItemChangedCallback != null) {
            OnItemChangedCallback.Invoke();
        }
    }

    public void Remove(string itemName)
    {
        Item item = null;
        foreach(Item i in items)
        {
            if(i.name == itemName)
            {
                item = i;
                continue;
            }
        }
        if(item != null)
        {
            Remove(item);
        }
    }

    [ContextMenu("defaultAdd")]
    public void CallDefaultAdd() {
        Add(DefaultItem);
    }

    [ContextMenu("defaultRemove")]
    public void CallDefaultRemove() {
        Remove(DefaultItem);
    }*/
}