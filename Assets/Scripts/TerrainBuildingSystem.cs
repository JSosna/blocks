using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainBuildingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject toolbar;
    private int selectedSlot = 0;

    [SerializeField]
    private Inventory inventory;

    private int blockSelectCounter = 0;

    public LayerMask groundLayer;

    float maxDist = 7;

    private float mouseHitInterval = .25f;
    private float timeToNextHit;
    private bool destroyButtonPressed = false;


    private void Start()
    {
        HandleSlotChange(selectedSlot);
    }


    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GamePaused || UI_Inventory.InventoryOpened) return;

        if (Input.GetMouseButtonDown(1))
            HandleMouseClick(false);
        if (Input.GetMouseButtonDown(0))
            HandleMouseClick(true);

        if(Input.GetMouseButtonUp(0)) {
            destroyButtonPressed = false;
        }

        else if (Input.GetMouseButton(0))
        {
            if(!destroyButtonPressed)
            {
                timeToNextHit = mouseHitInterval;
                destroyButtonPressed = true;
            }
            else
            {
                timeToNextHit -= 1f * Time.deltaTime;

                if(timeToNextHit <= 0)
                {
                    timeToNextHit = mouseHitInterval;
                    HandleMouseClick(true);
                }
            }
        }


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

        int oldIndex = 0;
        int newIndex = 0;

        for (int i=0; i<toolbar.transform.childCount; i++) {
            if (toolbar.transform.GetChild(i).name.Split(' ')[0] == selectedSlot.ToString()) {
                oldIndex = i;
            }


            if (toolbar.transform.GetChild(i).name.Split(' ')[0] == newSelection.ToString()) {
                newIndex = i;
            }
        }

        GameObject oldSlot = toolbar.transform.GetChild(oldIndex).gameObject;
        oldSlot.GetComponent<Image>().color = new Color32(200, 200, 200, 255);

        GameObject newSlot = toolbar.transform.GetChild(newIndex).gameObject;
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

            // destroy if left click - (button == true)
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

            // replace block with air if left click - (button == true)
            if (button)
            {
                
                if (biy != 0) // we can't destroy blocks on the bottom of the map
                {
                    if (tc.blocks[bix, biy, biz] == BlockType.DiamondOre)
                        Debug.Log("Diamonds, gg");

                    BlockType? block = tc.IncreaseBLockDestroyLevel(bix, biy, biz);
                    if(block.HasValue) {
                        if (block == BlockType.Dirt || block == BlockType.Grass || block == BlockType.GrassSnow) {
                            inventory.AddItem(new Item { itemType = ItemType.Dirt, amount = 1 });
                        }
                        else if (block == BlockType.Stone) {
                            inventory.AddItem(new Item { itemType = ItemType.Stone, amount = 1 });
                        }
                        else if(block == BlockType.Wood) {
                            inventory.AddItem(new Item { itemType = ItemType.Wood, amount = 1 });
                        }
                        else if(block == BlockType.Plank) {
                            inventory.AddItem(new Item { itemType = ItemType.Plank, amount = 1 });
                        }
                        else if(block == BlockType.Sand) {
                            inventory.AddItem(new Item { itemType = ItemType.Sand, amount = 1 });
                        }
                        else if(block == BlockType.IronOre) {
                            inventory.AddItem(new Item { itemType = ItemType.IronOre, amount = 1 });
                        }
                        else if(block == BlockType.Leaves) {
                            if(Random.Range(1, 13) == 1) {
                                inventory.AddItem(new Item { itemType = ItemType.Apple, amount = 1 });
                            }
                        }


                    }

                    tc.GenerateMesh();
                }
            }
            // right click
            else if (biy <= TerrainChunk.chunkHeight - 2) // and we can't place blocks above the limit
            {


                BlockType blockType = inventory.GetSlotItem(blockSelectCounter);
                if(blockType != BlockType.Air)
                    tc.blocks[bix, biy, biz] = blockType;



                tc.GenerateMesh();
            }
        }
    }
}
