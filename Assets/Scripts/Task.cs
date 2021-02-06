using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public string TaskContent { get; set; }
    public ItemType ItemType { get; set; }
    public int DesiredAmount { get; set; }
    public int CurrentAmount { get; set; }

    public Task(string title, ItemType itemType, int desiredAmount) {
        TaskContent = title;
        ItemType = itemType;
        DesiredAmount = desiredAmount;
        CurrentAmount = 0;
    }
}
