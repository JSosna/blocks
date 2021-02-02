using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    // Blocks
    public Sprite dirtSprite;
    public Sprite stoneSprite;
    public Sprite woodSprite;
    public Sprite plankSprite;
    public Sprite sandSprite;
    public Sprite ironOreSprite;
    public Sprite torchSprite;
    public Sprite furnaceSprite;

    // Food
    public Sprite appleSprite;

    // Other
    public Sprite stickSprite;
    public Sprite coalSprite;
    public Sprite ironSprite;
    public Sprite diamondSprite;

    // Tools
    public Sprite stonePickaxeSprite;
    public Sprite ironPickaxeSprite;
    public Sprite diamondPickaxeSprite;

    public Sprite stoneAxeSprite;
    public Sprite ironAxeSprite;
    public Sprite diamondAxeSprite;

    public Sprite stoneShovelSprite;
    public Sprite ironShovelSprite;
    public Sprite diamondShovelSprite;
}
