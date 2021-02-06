using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI AmountText;
    [SerializeField]
    private TextMeshProUGUI TaskContentText;

    private Inventory inventory;

    private List<Task> tasksToComplete = new List<Task>();
    private Task CurrentTask {
        get {
            if (tasksToComplete.Count > 0)
                return tasksToComplete[0];
            return null;
        }
    }


    private void Awake() {
        tasksToComplete.Add(new Task("Gather wood", ItemType.Wood, 5));
        tasksToComplete.Add(new Task("Find apples", ItemType.Apple, 3));
        tasksToComplete.Add(new Task("Mine stone", ItemType.Stone, 7));
        tasksToComplete.Add(new Task("Craft sticks", ItemType.Stick, 10));
        tasksToComplete.Add(new Task("Craft stone pickaxe", ItemType.StonePickaxe, 1));
        tasksToComplete.Add(new Task("Craft stone axe", ItemType.StoneAxe, 1));
        tasksToComplete.Add(new Task("Craft stone shovel", ItemType.StoneShovel, 1));
        tasksToComplete.Add(new Task("Find coal", ItemType.Coal, 4));
        tasksToComplete.Add(new Task("Craft torches", ItemType.Torch, 16));
        tasksToComplete.Add(new Task("Mine stones", ItemType.Stone, 10));
        tasksToComplete.Add(new Task("Craft Furnace", ItemType.Furnace, 1));
    }

    private void Start() {
        LoadTask();
    }

    internal void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e) {
        if(CurrentTask != null) {

            CurrentTask.CurrentAmount = inventory.GetItemTypeAmount(CurrentTask.ItemType);

            if (CurrentTask.CurrentAmount >= CurrentTask.DesiredAmount) {
                LoadNextTask();
            }
            else {
                AmountText.SetText(CurrentTask.CurrentAmount + " / " + CurrentTask.DesiredAmount);
            }
        }
    }

    private void LoadNextTask() {
        if(tasksToComplete.Count > 1) {
            tasksToComplete.RemoveAt(0);

            LoadTask();
        }
        else {
            gameObject.SetActive(false);
        }
    }

    private void LoadTask() {
        if (tasksToComplete.Count > 0) {

            if (inventory != null) {
                CurrentTask.CurrentAmount = inventory.GetItemTypeAmount(CurrentTask.ItemType);
                if (CurrentTask.CurrentAmount >= CurrentTask.DesiredAmount) {
                    LoadNextTask();
                    return;
                }
            }

            TaskContentText.SetText(CurrentTask.TaskContent);
            AmountText.SetText(CurrentTask.CurrentAmount + " / " + CurrentTask.DesiredAmount);
        }
    }
}
