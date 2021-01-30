using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;

    private Transform inventoryTransform;
    private Transform toolbarFrame;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();

        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        inventoryTransform = canvas.transform.Find("Inventory");
        toolbarFrame = canvas.transform.Find("ToolbarFrame");
    }

    private void Start() {
        rectTransform = GetComponent<RectTransform>();

        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        eventData.pointerDrag.transform.parent.SetAsLastSibling();

        if(eventData.pointerDrag.transform.parent.parent.name == "SlotBackgrounds") {
            inventoryTransform.SetAsLastSibling();
        } else {
            toolbarFrame.SetAsLastSibling();
        }
        

        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        rectTransform.position = rectTransform.parent.position;
    }
}
