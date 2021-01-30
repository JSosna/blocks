using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {
    [SerializeField]
    private Transform slotBackgrounds;
    [SerializeField]
    private Transform slotBackgroundsToolbar;
    [SerializeField]
    private Inventory inventory;


    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag != null) {

            string[] position = eventData.pointerDrag.name.Split(' ');
            int x = int.Parse(position[0]);
            int y = int.Parse(position[1]);

            Item itemMoved = new Item();

            foreach (Item item in inventory.items) {
                if (item.slot.x == x && item.slot.y == y) {
                    itemMoved = item;
                }
            }

            

            string[] newPosition = gameObject.transform.name.Split(' ');
            int newX = int.Parse(newPosition[0]);
            int newY = int.Parse(newPosition[1]);

            // Check if there already is and item at this position and swap them
            foreach (Item item in inventory.items) {
                if (item.slot.x == newX && item.slot.y == newY) {
                    item.slot.x = x;
                    item.slot.y = y;
                    
                }
            }

            GameObject oldSlot = new GameObject();
            // Get old slot
            if(y == 0) {
                for (int i = 0; i < slotBackgroundsToolbar.childCount; i++) {
                    if (slotBackgroundsToolbar.GetChild(i).name == eventData.pointerDrag.name) {
                        oldSlot = slotBackgroundsToolbar.GetChild(i).gameObject;
                    }
                }
            } else {
                for (int i = 0; i < slotBackgrounds.childCount; i++) {
                    if (slotBackgrounds.GetChild(i).name == eventData.pointerDrag.name) {
                        oldSlot = slotBackgrounds.GetChild(i).gameObject;
                    }
                }
            }


            for (int i = 0; i < slotBackgrounds.childCount; i++) {
                if (slotBackgrounds.GetChild(i).childCount > 0) {
                    if (slotBackgrounds.GetChild(i).GetChild(0).name == newX + " " + newY) {
                        var child = slotBackgrounds.GetChild(i).GetChild(0);
                        child.SetParent(oldSlot.transform);
                        child.GetComponent<RectTransform>().position = oldSlot.transform.position;
                        child.name = eventData.pointerDrag.name;
                    }
                }
            }

            for (int i = 0; i < slotBackgroundsToolbar.childCount; i++) {
                if (slotBackgroundsToolbar.GetChild(i).childCount > 0) {
                    if (slotBackgroundsToolbar.GetChild(i).GetChild(0).name == newX + " " + newY) {
                        var child = slotBackgroundsToolbar.GetChild(i).GetChild(0);
                        child.SetParent(oldSlot.transform);
                        child.GetComponent<RectTransform>().position = oldSlot.transform.position;
                        child.name = eventData.pointerDrag.name;
                    }
                }
            }


            eventData.pointerDrag.transform.SetParent(gameObject.transform);
            eventData.pointerDrag.name = gameObject.transform.name;
            eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.transform.position;
            itemMoved.slot.x = newX;
            itemMoved.slot.y = newY;
        }
    }
}