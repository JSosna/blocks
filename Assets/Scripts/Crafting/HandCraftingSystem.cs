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

        
        // Option 1 - wood -> plank
        var woodPlankCost = new Dictionary<Item, int>();
        woodPlankCost.Add(new Item { itemType = ItemType.Wood }, 1);
        CraftingOption woodPlank = new CraftingOption {
            CostItemsWithAmounts = woodPlankCost,
            ResultItem = new Item { itemType = ItemType.Plank },
            ResultItemAmount = 4
        };

        // Option 2 - plank -> stick
        var plankStickCost = new Dictionary<Item, int>();
        plankStickCost.Add(new Item { itemType = ItemType.Plank }, 2);
        CraftingOption plankStick = new CraftingOption {
            CostItemsWithAmounts = plankStickCost,
            ResultItem = new Item { itemType = ItemType.Stick },
            ResultItemAmount = 4
        };

        // Option 3 - stick + coal -> torch
        var stickCoalTorchCost = new Dictionary<Item, int>();
        stickCoalTorchCost.Add(new Item { itemType = ItemType.Stick }, 1);
        stickCoalTorchCost.Add(new Item { itemType = ItemType.Coal }, 1);
        CraftingOption stickCoalTorch = new CraftingOption {
            CostItemsWithAmounts = stickCoalTorchCost,
            ResultItem = new Item { itemType = ItemType.Torch },
            ResultItemAmount = 4
        };


        craftingOptions.Add(woodPlank);
        craftingOptions.Add(plankStick);
        craftingOptions.Add(stickCoalTorch);

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
