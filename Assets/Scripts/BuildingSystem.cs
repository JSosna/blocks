using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private Camera playerCamera;

    private bool buildModeOn = false;
    private bool canBuild = false;

    private BlockSystem blockSystem;

    [SerializeField]
    private LayerMask buildableSurfacesLayer;

    private Vector3 buildPos;

    private GameObject currentTemplateBlock;

    [SerializeField]
    private GameObject blockTemplatePrefab;
    [SerializeField]
    private GameObject blockPrefab;

    [SerializeField]
    private Material templateMaterial;

    private int blockSelectCounter = 0;

    private void Start()
    {
        blockSystem = GetComponent<BlockSystem>();
    }

    private void Update()
    {
        // change to pick blocks with keys (1-9) or scroll
        if (Input.GetKeyDown("1")) buildModeOn = !buildModeOn;

        if (Input.GetKeyDown("r"))
        {
            blockSelectCounter++;
            if (blockSelectCounter >= blockSystem.allBlocks.Count) blockSelectCounter = 0;
        }

        if (buildModeOn)
        {
            RaycastHit buildPosHit;

            if (Physics.Raycast(playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out buildPosHit, 8, buildableSurfacesLayer))
            {
                Vector3 point = buildPosHit.point;

                float xOffset;
                float zOffset;

                if (characterController.transform.rotation.eulerAngles.y > 0 && characterController.transform.rotation.eulerAngles.y < 180)
                    xOffset = -.1f;
                else
                    xOffset = .1f;

                if (characterController.transform.rotation.eulerAngles.y > 270 || characterController.transform.rotation.eulerAngles.y < 90)
                    zOffset = -.1f;
                else
                    zOffset = .1f;

                buildPos = new Vector3(Mathf.Round(point.x + xOffset), Mathf.FloorToInt(point.y+1f/2f), Mathf.Round(point.z + zOffset));
                canBuild = true;


                if (Input.GetMouseButtonDown(1))
                    Destroy(buildPosHit.transform.gameObject);
            }
            else
            {
                if (currentTemplateBlock != null)
                    Destroy(currentTemplateBlock.gameObject);
                    canBuild = false;
            }
        }


        if (!buildModeOn && currentTemplateBlock != null)
        {
            Destroy(currentTemplateBlock.gameObject);
            canBuild = false;
        }

        if (canBuild && currentTemplateBlock == null)
        {
            currentTemplateBlock = Instantiate(blockTemplatePrefab, buildPos, Quaternion.identity);
            currentTemplateBlock.GetComponent<MeshRenderer>().material = templateMaterial;
        }

        if (canBuild && currentTemplateBlock != null)
        {
            currentTemplateBlock.transform.position = buildPos;

            if (Input.GetMouseButtonDown(0))
                PlaceBlock();
        }
    }

    private void PlaceBlock()
    {
        GameObject newBlock = Instantiate(blockPrefab, buildPos, Quaternion.identity);
        Block tempBlock = blockSystem.allBlocks[blockSelectCounter];
        newBlock.name = tempBlock.blockName + "-Block";
        newBlock.GetComponent<MeshRenderer>().material = tempBlock.blockMaterial;
    }
}