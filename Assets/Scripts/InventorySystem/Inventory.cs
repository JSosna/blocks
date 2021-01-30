﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public event EventHandler OnItemListChanged;
    public List<Item> items;

    [SerializeField]
    private UI_Inventory ui_Inventory;


    [SerializeField]
    private PlayerHealth playerHealth;

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
                    item.slot = new Vector2Int(x, y);
                    items.Add(item);
                    return;
                }
            }      
    }

    public List<Item> GetItems() {
        return items;
    }

    public bool EatSlotItemIfEdible(int slotNumber) {
        foreach (Item item in items)
            if (item.slot.y == 0 && item.slot.x == slotNumber) {

                if (item.IsEdible()) {
                    if (playerHealth.Health < 10) {
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

    public void DeleteItem(Item item) {
        items.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
}