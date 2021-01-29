using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public event EventHandler OnItemListChanged;
    public List<Item> items;

    [SerializeField]
    private UI_Inventory ui_Inventory;


    private void Start()
    {
        items = new List<Item>();
        ui_Inventory.SetInventory(this);
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable()) {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in items) {
                if (inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory && items.Count < 40) {
                AddToFirstFreeSlot(item);
            }

        } else if(items.Count < 40)
            AddToFirstFreeSlot(item);

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddToFirstFreeSlot(Item item) {
        for (int y = 0; y < 5; y++)
            for (int x = 0; x < 8; x++) {
                bool slotTaken = false;

                foreach (Item itemInList in items) {
                    if (itemInList.slot.x == x && itemInList.slot.y == y)
                        slotTaken = true;
                }

                if(!slotTaken) {
                    Debug.Log("Slot: " + x + " " + y);
                    item.slot = new Vector2Int(x, y);
                    items.Add(item);
                    return;
                }
            }      
    }

    public List<Item> GetItems() {
        return items;
    }
}
