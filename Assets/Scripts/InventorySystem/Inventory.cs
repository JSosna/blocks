using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public event EventHandler OnItemListChanged;
    public List<Item> items = new List<Item>();

    [SerializeField]
    private UI_Inventory ui_Inventory;


    [SerializeField]
    private PlayerHealth playerHealth;

    private void Start() {
        ui_Inventory.SetInventory(this);

        AddItem(ItemType.IronAxe, 1);
        AddItem(ItemType.StonePickaxe, 1);
        AddItem(ItemType.DiamondShovel, 1);
    }

    public void AddItem(Item item) {
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

    public void AddItem(ItemType itemType, int amount) {
        for(int i=0; i<items.Count; i++) {
            if(items[i].itemType == itemType) {
                items[i].amount += amount;
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return;
            }
        }

        AddToFirstFreeSlot(itemType, amount);

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
                    item.slot = new Vector2Int(x, y);
                    items.Add(item);
                    return;
                }
            }      
    }

    public void AddToFirstFreeSlot(ItemType itemType, int amount) {
        for (int y = 0; y < 5; y++)
            for (int x = 0; x < 8; x++) {
                bool slotTaken = false;

                foreach (Item itemInList in items) {
                    if (itemInList.slot.x == x && itemInList.slot.y == y)
                        slotTaken = true;
                }

                if (!slotTaken) {
                    var item = new Item { itemType = itemType, amount = amount };
                    item.slot = new Vector2Int(x, y);
                    items.Add(item);
                    return;
                }
            }
    }

    public List<Item> GetItems() {
        return items;
    }

    public bool CheckIfGotRequiredItemTypeWithAmount(ItemType itemType, int amount) {
        for (int i = 0; i < items.Count; i++)
            if (items[i].itemType == itemType && items[i].amount >= amount)
                return true;
        return false;
    }

    public void SubtractItem(ItemType itemType, int amount) {
        for (int i = 0; i < items.Count; i++)
            if (items[i].itemType == itemType) {

                items[i].amount -= amount;

                if (items[i].amount == 0) {
                    DeleteItem(items[i]);
                }
            }

        OnItemListChanged.Invoke(this, EventArgs.Empty);
    }

    

    public bool EatSlotItemIfEdible(int slotNumber) {
        foreach (Item item in items)
            if (item.slot.y == 0 && item.slot.x == slotNumber) {

                if (item.IsEdible()) {
                    if (playerHealth.Health < 10) {
                        Debug.Log("Inventory adding 2");
                        playerHealth.Health += 2;

                        item.amount--;

                        if (item.amount == 0)
                            items.Remove(item);

                        OnItemListChanged.Invoke(this, EventArgs.Empty);
                    }
                        

                    return true;
                }
            }
        return false;
    }
    
    public bool IsSlotItemPlaceable(int slotNumber) {
        foreach (Item item in items)
            if (item.slot.y == 0 && item.slot.x == slotNumber)
                    return item.IsPlacealbe();
        return false;
    }

    public BlockType GetSlotItem(int slotNumber) {

        foreach (Item item in items)
            if (item.slot.y == 0 && item.slot.x == slotNumber) {

                ItemType itemType = item.itemType;

                item.amount--;

                if(item.amount == 0)
                    items.Remove(item);

                OnItemListChanged.Invoke(this, EventArgs.Empty);

                return Item.GetBlockType(itemType);
            }
        return BlockType.Air;
    }


    public ItemType GetItemTypeInSlot(int slotNumber) {
        foreach (Item item in items)
            if (item.slot.y == 0 && item.slot.x == slotNumber)
                return item.itemType;
        // Todo - change
        return ItemType.Stick;
    }


    public void DeleteItem(Item item) {
        items.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
}
