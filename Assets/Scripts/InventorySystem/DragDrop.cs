using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;

    private Transform inventory;
    private Transform toolbarFrame;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();

        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        inventory = canvas.transform.Find("Inventory");
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
            inventory.SetAsLastSibling();
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

    public void OnPointerDown(PointerEventData eventData) {
    }
}
