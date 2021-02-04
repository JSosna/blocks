using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    // Blocks
    Dirt,
    Stone,
    Wood,
    Plank,
    Sand,
    Wool,
    IronOre,
    Furnace,
    Torch,
    Glass,

    // Edible
    Apple,
    Mutton,
    CookedMutton,

    // Other (crafting)
    Stick,
    Coal,
    Iron,
    Diamond,

    // Tools
    StonePickaxe,
    IronPickaxe,
    DiamondPickaxe,

    StoneAxe,
    IronAxe,
    DiamondAxe,

    StoneShovel,
    IronShovel,
    DiamondShovel
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
            case ItemType.Plank: return ItemAssets.Instance.plankSprite;
            case ItemType.Sand: return ItemAssets.Instance.sandSprite;
            case ItemType.Wool: return ItemAssets.Instance.woolSprite;
            case ItemType.IronOre: return ItemAssets.Instance.ironOreSprite;
            case ItemType.Furnace: return ItemAssets.Instance.furnaceSprite;
            case ItemType.Glass: return ItemAssets.Instance.glassSprite;

            case ItemType.Torch: return ItemAssets.Instance.torchSprite;

            case ItemType.Apple: return ItemAssets.Instance.appleSprite;
            case ItemType.Mutton: return ItemAssets.Instance.muttonSprite;
            case ItemType.CookedMutton: return ItemAssets.Instance.cookedMuttonSprite;

            case ItemType.Stick: return ItemAssets.Instance.stickSprite;
            case ItemType.Coal: return ItemAssets.Instance.coalSprite;
            case ItemType.Iron: return ItemAssets.Instance.ironSprite;
            case ItemType.Diamond: return ItemAssets.Instance.diamondSprite;

            case ItemType.StonePickaxe: return ItemAssets.Instance.stonePickaxeSprite;
            case ItemType.IronPickaxe: return ItemAssets.Instance.ironPickaxeSprite;
            case ItemType.DiamondPickaxe: return ItemAssets.Instance.diamondPickaxeSprite;

            case ItemType.StoneAxe: return ItemAssets.Instance.stoneAxeSprite;
            case ItemType.IronAxe: return ItemAssets.Instance.ironAxeSprite;
            case ItemType.DiamondAxe: return ItemAssets.Instance.diamondAxeSprite;

            case ItemType.StoneShovel: return ItemAssets.Instance.stoneShovelSprite;
            case ItemType.IronShovel: return ItemAssets.Instance.ironShovelSprite;
            case ItemType.DiamondShovel: return ItemAssets.Instance.diamondShovelSprite;
        }
    }
        
    public bool IsStackable() {
        switch(itemType) {
            default:
                return true;

            case ItemType.Furnace:
            case ItemType.StonePickaxe:
            case ItemType.IronPickaxe:
            case ItemType.DiamondPickaxe:
                return false;
        }
    }

    public bool IsEdible() {
        switch (itemType) {
            default:
                return false;

            case ItemType.Apple:
            case ItemType.CookedMutton:
                return true;
        }
    }

    public bool IsTool() {
        switch (itemType) {
            default:
                return false;

            case ItemType.StonePickaxe:
            case ItemType.IronPickaxe:
            case ItemType.DiamondPickaxe:

            case ItemType.StoneAxe:
            case ItemType.IronAxe:
            case ItemType.DiamondAxe:

            case ItemType.StoneShovel:
            case ItemType.IronShovel:
            case ItemType.DiamondShovel:
                return true;
        }
    }

    public bool IsPlacealbe() {
        switch (itemType) {
            default:
                return false;

            case ItemType.Dirt:
            case ItemType.Stone:
            case ItemType.Wood:
            case ItemType.Plank:
            case ItemType.Sand:
            case ItemType.Wool:
            case ItemType.IronOre:
            case ItemType.Furnace:
            case ItemType.Glass:
            case ItemType.Torch:
                return true;
        }
    }


    public static BlockType GetBlockType(ItemType itemType) {
        switch(itemType) {
            case ItemType.Dirt:
                return BlockType.Dirt;
            case ItemType.Stone:
                return BlockType.Stone;
            case ItemType.Wood:
                return BlockType.Wood;
            case ItemType.Plank:
                return BlockType.Plank;
            case ItemType.Sand:
                return BlockType.Sand;
            case ItemType.Wool:
                return BlockType.WoolWhite;
            case ItemType.IronOre:
                return BlockType.IronOre;
            case ItemType.Furnace:
                return BlockType.Furnace;
            case ItemType.Glass:
                return BlockType.Glass;
            case ItemType.Torch:
                return BlockType.Torch;
            default:
                return BlockType.Air;
        }
    }
}
