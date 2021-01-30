using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem
{
    public const int GRID_SIZE = 3;

    public Item[,] craftingItems;
    
    public CraftingSystem() {
        craftingItems = new Item[GRID_SIZE, GRID_SIZE];
    }

    public bool IsEmpty(int x, int y) {
        return craftingItems[x, y] == null;
    }

    public Item GetItem(int x, int y) {
        return craftingItems[x, y];
    }

    public void SetItem(Item item, int x, int y) {
        craftingItems[x, y] = item;
    }

    public void IncreaseItemAmount(int x, int y) {
        GetItem(x, y).amount--;
    }

    public void DecreaseItemAmount(int x, int y) {
        GetItem(x, y).amount++;
    }

    public void RemoveItem(int x, int y) {
        SetItem(null, x, y);
    }

    public bool TryAddItem(Item item, int x, int y) {
        if(IsEmpty(x, y)) {
            SetItem(item, x, y);
            return true;
        } else {
            if(item.itemType == GetItem(x, y).itemType) {
                IncreaseItemAmount(x, y);
                return true;
            }
        }
        return false;
    }
}
