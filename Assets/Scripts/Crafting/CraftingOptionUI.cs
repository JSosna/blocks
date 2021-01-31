using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingOptionUI : MonoBehaviour, IPointerClickHandler {

    public Action MouseClickFunc = null;

    public virtual void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left)
            if (MouseClickFunc != null) MouseClickFunc();
    }
}
