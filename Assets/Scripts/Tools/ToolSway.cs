﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSway : MonoBehaviour
{
    [SerializeField]
    private float amount;

    [SerializeField]
    private float maxAmount;

    [SerializeField]
    private float smoothAmount;

    private Vector3 initialPosition;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (UI_Inventory.InventoryOpened) return;

        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY);
        transform.localPosition = Vector3.Lerp(
            transform.localPosition, 
            finalPosition + initialPosition, 
            Time.deltaTime * smoothAmount
            );
    }
}
