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

    // Food
    public Sprite appleSprite;

    // Other
    public Sprite stickSprite;
    public Sprite coalSprite;

    // Tools
    public Sprite stonePickaxeSprite;
    public Sprite ironPickaxeSprite;
    public Sprite diamondPickaxeSprite;
}
