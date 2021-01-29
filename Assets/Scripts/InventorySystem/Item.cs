using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Dirt,
    Stone,
    Wood
}

public class Item
{
    public ItemType itemType;
    public int amount;
    public Vector2Int slot;

    public Sprite GetSprite() {
        switch(itemType) {
            default:
            case ItemType.Dirt: return ItemAssets.Instance.dirtSprite;
            case ItemType.Stone: return ItemAssets.Instance.stoneSprite;
            case ItemType.Wood: return ItemAssets.Instance.woodSprite;
        }
    }

    public bool IsStackable() {
        switch(itemType) {
            default:
            //case ItemType.Dirt:
            case ItemType.Stone:
            case ItemType.Wood:
                return true;
            case ItemType.Dirt:
                return false;
        }
    }
}
