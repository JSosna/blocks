using System;
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

    public GameObject stoneAxeObject;
    public GameObject ironAxeObject;
    public GameObject diamondAxeObject;

    public GameObject stoneShovelObject;
    public GameObject ironShovelObject;
    public GameObject diamondShovelObject;

    public const string HandToolName = "Hand";
    public const float HandHitSpeed = .5f;


    public string GetToolGameObjectName(ItemType tool) {
        switch (tool) {
            default:

            case ItemType.StonePickaxe: return stonePickaxeObject.name;
            case ItemType.IronPickaxe: return ironPickaxeObject.name;
            case ItemType.DiamondPickaxe: return diamondPickaxeObject.name;

            case ItemType.StoneAxe: return stoneAxeObject.name;
            case ItemType.IronAxe: return ironAxeObject.name;
            case ItemType.DiamondAxe: return diamondAxeObject.name;

            case ItemType.StoneShovel: return stoneShovelObject.name;
            case ItemType.IronShovel: return ironShovelObject.name;
            case ItemType.DiamondShovel: return diamondShovelObject.name;
        }
    }

    public float GetToolHitSpeed(ItemType tool) {
        switch (tool) {
            default: 

            case ItemType.StonePickaxe: return .4f;
            case ItemType.IronPickaxe: return .3f;
            case ItemType.DiamondPickaxe: return .25f;

            case ItemType.StoneAxe: return .4f;
            case ItemType.IronAxe: return .3f;
            case ItemType.DiamondAxe: return .25f;

            case ItemType.StoneShovel: return .4f;
            case ItemType.IronShovel: return .3f;
            case ItemType.DiamondShovel: return .25f;

        }
    }
}
