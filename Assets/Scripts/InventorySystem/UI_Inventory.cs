using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour {
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform slotBackgrounds;
    private Transform slotBackgroundsToolbar;

    [SerializeField]
    private Transform toolbar;

    [SerializeField]
    private GameObject frame;

    [SerializeField]
    private GameObject[] slots;

    public static bool InventoryOpened { get; set; }


    private void Awake() {
        itemSlotContainer = transform.Find("Frame").Find("Background").Find("SlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemTemplate");

        slotBackgrounds = itemSlotContainer.Find("SlotBackgrounds");
        slotBackgroundsToolbar = toolbar.Find("SlotBackgroundsToolbar");
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

        foreach (Transform child in slotBackgrounds) {
            foreach (Transform childOfChild in child) {
                Destroy(childOfChild.gameObject);
            }
        }

        foreach (Transform child in slotBackgroundsToolbar) {
            foreach(Transform childOfChild in child) {
                Destroy(childOfChild.gameObject);
            }
        }

        foreach (Item item in inventory.GetItems()) {
            if (item.slot.y >= 5) return;

            RectTransform itemSlotRectTransform = new RectTransform();
            if (item.slot.y == 0) {
                string str = item.slot.x + " " + item.slot.y;

                for (int i = 0; i < slotBackgroundsToolbar.childCount; i++) {
                    if(slotBackgroundsToolbar.GetChild(i).name == str) {
                        itemSlotRectTransform = Instantiate(itemSlotTemplate, slotBackgroundsToolbar.GetChild(i)).GetComponent<RectTransform>();
                        itemSlotRectTransform.position = slotBackgroundsToolbar.GetChild(i).position;
                    }
                }
            }
            else {
                string str = item.slot.x + " " + item.slot.y;

                for (int i = 0; i < slotBackgrounds.childCount; i++) {
                    if (slotBackgrounds.GetChild(i).name == str) {
                        itemSlotRectTransform = Instantiate(itemSlotTemplate, slotBackgrounds.GetChild(i)).GetComponent<RectTransform>();
                        itemSlotRectTransform.position = slotBackgrounds.GetChild(i).position;
                    }
                }
            }

            if(itemSlotRectTransform != null) {
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.gameObject.AddComponent<DragDrop>();
                itemSlotRectTransform.gameObject.AddComponent<CanvasGroup>();
                itemSlotRectTransform.name = item.slot.x + " " + item.slot.y;

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
}
