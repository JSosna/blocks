﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public Sprite dirtSprite;
    public Sprite stoneSprite;
    public Sprite woodSprite;
}
