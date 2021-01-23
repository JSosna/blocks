using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainBuildingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject toolbar;
    private int selectedSlot = 0;

    private int blockSelectCounter = 0;

    public LayerMask groundLayer;

    float maxDist = 7;

    private void Start()
    {
        HandleSlotChange(selectedSlot);
    }


    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
            HandleMouseClick(false);
        else if (Input.GetMouseButtonDown(1))
            HandleMouseClick(true);


        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
        {
            if (selectedSlot < 8)
                HandleSlotChange(selectedSlot + 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
        {
            if (selectedSlot > 0)
                HandleSlotChange(selectedSlot - 1);
        }


        if (Input.GetKeyDown("1")) HandleSlotChange(0);
        if (Input.GetKeyDown("2")) HandleSlotChange(1);
        if (Input.GetKeyDown("3")) HandleSlotChange(2);
        if (Input.GetKeyDown("4")) HandleSlotChange(3);
        if (Input.GetKeyDown("5")) HandleSlotChange(4);
        if (Input.GetKeyDown("6")) HandleSlotChange(5);
        if (Input.GetKeyDown("7")) HandleSlotChange(6);
        if (Input.GetKeyDown("8")) HandleSlotChange(7);
        if (Input.GetKeyDown("9")) HandleSlotChange(8);
    }


    private void HandleSlotChange(int newSelection)
    {
        blockSelectCounter = newSelection;

        GameObject oldSlot = toolbar.transform.GetChild(selectedSlot).gameObject;
        oldSlot.GetComponent<Image>().color = new Color32(0, 0, 0, 255);

        GameObject newSlot = toolbar.transform.GetChild(newSelection).gameObject;
        newSlot.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        selectedSlot = newSelection;
    }

    /// <summary>
    /// Function that handles mouse click in building mode
    /// </summary>
    /// <param name="button">
    /// 0 - false - left
    /// 1 - true - right
    /// </param>
    private void HandleMouseClick(bool button)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDist, groundLayer))
        {
            Vector3 pointInTargetBlock;

            // destroy if right click - (button == true)
            if (button)
                pointInTargetBlock = hitInfo.point + transform.forward * .01f;
            else
                pointInTargetBlock = hitInfo.point - transform.forward * .01f;

            // get the terrain chunk
            int chunkPosX = Mathf.FloorToInt(pointInTargetBlock.x / TerrainChunk.chunkWidth) * TerrainChunk.chunkWidth;
            int chunkPosZ = Mathf.FloorToInt(pointInTargetBlock.z / TerrainChunk.chunkWidth) * TerrainChunk.chunkWidth;

            Vector2Int cp = new Vector2Int(chunkPosX, chunkPosZ);

            TerrainChunk tc = TerrainGenerator.chunks[cp];

            // index of the target block
            int bix = Mathf.FloorToInt(pointInTargetBlock.x) - chunkPosX + 1;
            int biy = Mathf.FloorToInt(pointInTargetBlock.y);
            int biz = Mathf.FloorToInt(pointInTargetBlock.z) - chunkPosZ + 1;

            // replace block with air if right click - (button == true)
            if (button)
            {
                
                if (biy != 0) // we can't destroy blocks on the bottom of the map
                {
                    tc.blocks[bix, biy, biz] = BlockType.Air;
                    tc.GenerateMesh();
                }
            }
            // left click
            else if (biy <= TerrainChunk.chunkHeight - 2) // and we can't place blocks above the limit
            {
                tc.blocks[bix, biy, biz] = (BlockType)(blockSelectCounter + 2);
                tc.GenerateMesh();
            }
        }
    }
}
