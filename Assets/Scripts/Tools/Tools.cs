using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {
    public static Tools Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public GameObject stonePickaxeObject;
    public GameObject ironPickaxeObject;
    public GameObject diamondPickaxeObject;

    public const string HandToolName = "Hand";
    public const float HandHitSpeed = .5f;



    public string GetToolGameObjectName(ItemType tool) {
        switch (tool) {
            default:

            case ItemType.StonePickaxe: return stonePickaxeObject.name;
            case ItemType.IronPickaxe: return ironPickaxeObject.name;
            case ItemType.DiamondPickaxe: return diamondPickaxeObject.name;
        }
    }

    public float GetToolHitSpeed(ItemType tool) {
        switch (tool) {
            default: 

            case ItemType.StonePickaxe: return .4f;
            case ItemType.IronPickaxe: return .3f;
            case ItemType.DiamondPickaxe: return .25f;
        }
    }
}
