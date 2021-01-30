using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Item : MonoBehaviour, IPointerClickHandler {

    public Action MouseMiddleClickFunc = null;

    public virtual void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Middle)
            if (MouseMiddleClickFunc != null) MouseMiddleClickFunc();
    }
}