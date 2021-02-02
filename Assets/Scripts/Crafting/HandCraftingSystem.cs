using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HandCraftingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject CraftingOptionTemplate;

    private CraftingOption[] HandCraftOptions;

    private List<Transform> HandCraftOptionsTransforms = new List<Transform>();

    [SerializeField]
    private Inventory inventory;

    public void Start() {
        HandCraftOptions = LoadHandCraftOptions();

        LoadCraftingMenu();
        SelectCraftableOptions();
    }

    private CraftingOption[] LoadHandCraftOptions() {
        List<CraftingOption> craftingOptions = new List<CraftingOption>();

        
        // wood -> 4 planks
        var plankCost = new Dictionary<Item, int>();
        plankCost.Add(new Item { itemType = ItemType.Wood }, 1);
        CraftingOption plank = new CraftingOption {
            CostItemsWithAmounts = plankCost,
            ResultItem = new Item { itemType = ItemType.Plank },
            ResultItemAmount = 4
        };

        // plank -> 4 sticks
        var stickCost = new Dictionary<Item, int>();
        stickCost.Add(new Item { itemType = ItemType.Plank }, 2);
        CraftingOption stick = new CraftingOption {
            CostItemsWithAmounts = stickCost,
            ResultItem = new Item { itemType = ItemType.Stick },
            ResultItemAmount = 4
        };

        // stick + coal -> 4 torches
        var torchCost = new Dictionary<Item, int>();
        torchCost.Add(new Item { itemType = ItemType.Stick }, 1);
        torchCost.Add(new Item { itemType = ItemType.Coal }, 1);
        CraftingOption torch = new CraftingOption {
            CostItemsWithAmounts = torchCost,
            ResultItem = new Item { itemType = ItemType.Torch },
            ResultItemAmount = 4
        };

        // 10 stone -> furnace
        var furnaceCost = new Dictionary<Item, int>();
        furnaceCost.Add(new Item { itemType = ItemType.Stone }, 10);
        CraftingOption furnace = new CraftingOption {
            CostItemsWithAmounts = furnaceCost,
            ResultItem = new Item { itemType = ItemType.Furnace },
            ResultItemAmount = 1
        };

        // 2 sticks + 3 stones -> stone pickaxe
        var stonePickaxeCost = new Dictionary<Item, int>();
        stonePickaxeCost.Add(new Item { itemType = ItemType.Stick }, 2);
        stonePickaxeCost.Add(new Item { itemType = ItemType.Stone }, 3);
        CraftingOption stonePickaxe = new CraftingOption {
            CostItemsWithAmounts = stonePickaxeCost,
            ResultItem = new Item { itemType = ItemType.StonePickaxe },
            ResultItemAmount = 1
        };

        // 2 sticks + 3 stones -> stone axe
        var stoneAxeCost = new Dictionary<Item, int>();
        stoneAxeCost.Add(new Item { itemType = ItemType.Stick }, 2);
        stoneAxeCost.Add(new Item { itemType = ItemType.Stone }, 3);
        CraftingOption stoneAxe = new CraftingOption {
            CostItemsWithAmounts = stoneAxeCost,
            ResultItem = new Item { itemType = ItemType.StoneAxe },
            ResultItemAmount = 1
        };

        // 2 sticks + 1 stone -> stone shovel
        var stoneShovelCost = new Dictionary<Item, int>();
        stoneShovelCost.Add(new Item { itemType = ItemType.Stick }, 2);
        stoneShovelCost.Add(new Item { itemType = ItemType.Stone }, 1);
        CraftingOption stoneShovel = new CraftingOption {
            CostItemsWithAmounts = stoneShovelCost,
            ResultItem = new Item { itemType = ItemType.StoneShovel },
            ResultItemAmount = 1
        };

        // 2 sticks + 3 iron -> iron pickaxe
        var ironPickaxeCost = new Dictionary<Item, int>();
        ironPickaxeCost.Add(new Item { itemType = ItemType.Stick }, 2);
        ironPickaxeCost.Add(new Item { itemType = ItemType.Iron }, 3);
        CraftingOption ironPickaxe = new CraftingOption {
            CostItemsWithAmounts = ironPickaxeCost,
            ResultItem = new Item { itemType = ItemType.IronPickaxe },
            ResultItemAmount = 1
        };

        // 2 sticks + 3 iron -> iron axe
        var ironAxeCost = new Dictionary<Item, int>();
        ironAxeCost.Add(new Item { itemType = ItemType.Stick }, 2);
        ironAxeCost.Add(new Item { itemType = ItemType.Iron }, 3);
        CraftingOption ironAxe = new CraftingOption {
            CostItemsWithAmounts = ironAxeCost,
            ResultItem = new Item { itemType = ItemType.IronAxe },
            ResultItemAmount = 1
        };

        // 2 sticks + 1 iron -> iron shovel
        var ironShovelCost = new Dictionary<Item, int>();
        ironShovelCost.Add(new Item { itemType = ItemType.Stick }, 2);
        ironShovelCost.Add(new Item { itemType = ItemType.Iron }, 1);
        CraftingOption ironShovel = new CraftingOption {
            CostItemsWithAmounts = ironShovelCost,
            ResultItem = new Item { itemType = ItemType.IronShovel },
            ResultItemAmount = 1
        };

        // 2 sticks + 3 diamonds -> diamond pickaxe
        var diamondPickaxeCost = new Dictionary<Item, int>();
        diamondPickaxeCost.Add(new Item { itemType = ItemType.Stick }, 2);
        diamondPickaxeCost.Add(new Item { itemType = ItemType.Diamond }, 3);
        CraftingOption diamondPickaxe = new CraftingOption {
            CostItemsWithAmounts = diamondPickaxeCost,
            ResultItem = new Item { itemType = ItemType.DiamondPickaxe },
            ResultItemAmount = 1
        };

        // 2 sticks + 3 diamonds -> diamond axe
        var diamondAxeCost = new Dictionary<Item, int>();
        diamondAxeCost.Add(new Item { itemType = ItemType.Stick }, 2);
        diamondAxeCost.Add(new Item { itemType = ItemType.Diamond }, 3);
        CraftingOption diamondAxe = new CraftingOption {
            CostItemsWithAmounts = diamondAxeCost,
            ResultItem = new Item { itemType = ItemType.DiamondAxe },
            ResultItemAmount = 1
        };

        // 2 sticks + 1 diamond -> diamond shovel
        var diamondShovelCost = new Dictionary<Item, int>();
        diamondShovelCost.Add(new Item { itemType = ItemType.Stick }, 2);
        diamondShovelCost.Add(new Item { itemType = ItemType.Diamond }, 1);
        CraftingOption diamondShovel = new CraftingOption {
            CostItemsWithAmounts = diamondShovelCost,
            ResultItem = new Item { itemType = ItemType.DiamondShovel },
            ResultItemAmount = 1
        };

        craftingOptions.Add(plank);
        craftingOptions.Add(stick);
        craftingOptions.Add(torch);
        craftingOptions.Add(furnace);

        craftingOptions.Add(stonePickaxe);
        craftingOptions.Add(ironPickaxe);
        craftingOptions.Add(diamondPickaxe);

        craftingOptions.Add(stoneAxe);
        craftingOptions.Add(ironAxe);
        craftingOptions.Add(diamondAxe);

        craftingOptions.Add(stoneShovel);
        craftingOptions.Add(ironShovel);
        craftingOptions.Add(diamondShovel);

        return craftingOptions.ToArray();
    }

    private void LoadCraftingMenu() {
        HandCraftOptionsTransforms.Clear();

        for (int i = 0; i < HandCraftOptions.Length; i++) {
            var craftingOptionUI = Instantiate(CraftingOptionTemplate, this.transform);
            HandCraftOptionsTransforms.Add(craftingOptionUI.transform);

            craftingOptionUI.name = i.ToString();
            craftingOptionUI.SetActive(true);

            var resultItem = craftingOptionUI.transform.Find("ResultItem");
            // Set image of crafting option
            resultItem.GetComponent<Image>().sprite = HandCraftOptions[i].ResultItem.GetSprite();

            // Set amount of result item created
            int amount = HandCraftOptions[i].ResultItemAmount;

            if (amount > 1)
                resultItem.Find("Amount").GetComponent<TextMeshProUGUI>().SetText(amount.ToString());
            else
                resultItem.Find("Amount").GetComponent<TextMeshProUGUI>().SetText("");

            craftingOptionUI.GetComponent<CraftingOptionUI>().MouseClickFunc = () => {
                CraftItem(int.Parse(craftingOptionUI.name));
            };
        }
    }

    private void CraftItem(int craftOptionNumber) {
        bool canCraft = true;

        for (int x = 0; x < HandCraftOptions[craftOptionNumber].CostItemsWithAmounts.Count; x++) {
            ItemType requiredItemType = HandCraftOptions[craftOptionNumber].CostItemsWithAmounts.ElementAt(x).Key.itemType;
            int requiredAmount = HandCraftOptions[craftOptionNumber].CostItemsWithAmounts.ElementAt(x).Value;

            if (!inventory.CheckIfGotRequiredItemTypeWithAmount(requiredItemType, requiredAmount))
                canCraft = false;
        }

        if (canCraft) {
            for (int x = 0; x < HandCraftOptions[craftOptionNumber].CostItemsWithAmounts.Count; x++) {
                ItemType itemType = HandCraftOptions[craftOptionNumber].CostItemsWithAmounts.ElementAt(x).Key.itemType;
                int amount = HandCraftOptions[craftOptionNumber].CostItemsWithAmounts.ElementAt(x).Value;

                inventory.SubtractItem(itemType, amount);
            }

            inventory.AddItem(HandCraftOptions[craftOptionNumber].ResultItem.itemType, HandCraftOptions[craftOptionNumber].ResultItemAmount);
        }
    }

    public void SelectCraftableOptions() {
        for(int i = 0; i< HandCraftOptionsTransforms.Count; i++) {
            bool canCraft = true;
            
            for (int x = 0; x < HandCraftOptions[i].CostItemsWithAmounts.Count; x++) {
                ItemType requiredItemType = HandCraftOptions[i].CostItemsWithAmounts.ElementAt(x).Key.itemType;
                int requiredAmount = HandCraftOptions[i].CostItemsWithAmounts.ElementAt(x).Value;

                if (!inventory.CheckIfGotRequiredItemTypeWithAmount(requiredItemType, requiredAmount))
                    canCraft = false;
            }

            if (canCraft) {
                HandCraftOptionsTransforms[i].GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            } else {
                HandCraftOptionsTransforms[i].GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            }
        }
    }

}
