using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour {
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    [SerializeField]
    private Transform toolbar;

    [SerializeField]
    private GameObject frame;

    public static bool InventoryOpened { get; set; }


    private void Awake() {
        itemSlotContainer = transform.Find("Frame").Find("Background").Find("SlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemTemplate");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Tab)) {
            if (InventoryOpened) {
                CloseInventory();
            }
            else {
                OpenInventory();
            }

            InventoryOpened = !InventoryOpened;
        }
    }

    private void OpenInventory() {
        frame.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CloseInventory() {
        frame.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems() {
        foreach (Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate || child.name == "SlotBackgrounds") continue;
            Destroy(child.gameObject);
        }

        foreach (Transform child in toolbar) {
            if (child.name == "SlotBackgrounds") continue;
            Destroy(child.gameObject);
        }

        float itemSlotCellSize = 100f;

        foreach (Item item in inventory.GetItems()) {
            if (item.slot.y >= 5) return;

            RectTransform itemSlotRectTransform;
            if (item.slot.y == 0) {
                itemSlotRectTransform = Instantiate(itemSlotTemplate, toolbar).GetComponent<RectTransform>();
            }
            else
                itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.gameObject.AddComponent<DragDrop>();
            itemSlotRectTransform.gameObject.AddComponent<CanvasGroup>();

            if(item.slot.y == 0)
                itemSlotRectTransform.anchoredPosition = new Vector2(item.slot.x * itemSlotCellSize + 60, -item.slot.y * itemSlotCellSize - 55);
            else
                itemSlotRectTransform.anchoredPosition = new Vector2(item.slot.x * itemSlotCellSize - 300, -item.slot.y * itemSlotCellSize + 200);

            Image image = itemSlotRectTransform.GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI amount = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
                amount.SetText(item.amount.ToString());
            else
                amount.SetText("");
        }
    }
}
