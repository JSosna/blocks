using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Crafting : MonoBehaviour
{
    [SerializeField]
    private Transform itemSlotTemplate;

    public Transform[,] craftingSlots;
    private Transform resultSlot;

    private void Start() {
        craftingSlots = new Transform[CraftingSystem.GRID_SIZE, CraftingSystem.GRID_SIZE];
        initializeCraftingSlots();

        resultSlot = transform.Find("CraftingResultSlot");

        CreateItem(new Item { itemType = ItemType.Apple }, 0, 0);
        CreateItem(new Item { itemType = ItemType.Sand }, 1, 1);
        CreateResultItem(new Item { itemType = ItemType.Dirt });
    }

    private void initializeCraftingSlots() {
        Transform craftingSlotContainer = transform.Find("CraftingSlotContainer");

        for (int x = 0; x < CraftingSystem.GRID_SIZE; x++)
            for (int y = 0; y < CraftingSystem.GRID_SIZE; y++) {
                craftingSlots[x, y] = craftingSlotContainer.Find(x + " " + y + " 1");
            }
    }

    private void CreateItem(Item item, int x, int y) {
        Transform itemTransform = Instantiate(itemSlotTemplate, craftingSlots[x, y]);
        itemTransform.gameObject.SetActive(true);
        itemTransform.position = craftingSlots[x, y].position;
        itemTransform.GetComponent<Image>().sprite = item.GetSprite();
        itemTransform.gameObject.AddComponent<CanvasGroup>();
    }

    private void CreateResultItem(Item item) {
        Transform itemTransform = Instantiate(itemSlotTemplate, resultSlot);
        itemTransform.gameObject.SetActive(true);
        itemTransform.position = resultSlot.position;
        itemTransform.GetComponent<Image>().sprite = item.GetSprite();
        itemTransform.gameObject.AddComponent<CanvasGroup>();
    }
}
