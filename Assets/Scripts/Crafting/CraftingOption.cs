using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingOption {
    public Dictionary<Item, int> CostItemsWithAmounts { get; set; }
    public Item ResultItem { get; set; }
    public int ResultItemAmount { get; set; }
}
