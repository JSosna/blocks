using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    private float mouseHitInterval = .5f;
    private float timeToNextHit;
    private bool destroyButtonPressed = false;

    [SerializeField]
    private GameObject torchLightPrefab;

    [SerializeField]
    private GameObject blockHitParticlePrefab;

    [SerializeField]
    private Tools tools;

    private string currentToolName = "Hand";
    private Tools.ToolType currentTool;
    private bool holdingTool = false;

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
        if (Input.GetMouseButtonDown(0)) {
            HandleMouseClick(true);
        }

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
            if (selectedSlot < 7) {

                HandleSlotChange(selectedSlot + 1);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
        {
            if (selectedSlot > 0) {
                HandleSlotChange(selectedSlot - 1);
            }
                
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

        // Check if there is a tool in the new slot
        ItemType itemTypeInSelectedSlot = inventory.GetItemTypeInSlot(blockSelectCounter);

        var itemType = new Item { itemType = itemTypeInSelectedSlot };

        if (itemType.IsTool()) {
            holdingTool = true;
            currentTool = tools.GetToolType(itemTypeInSelectedSlot);
            currentToolName = tools.GetToolGameObjectName(itemTypeInSelectedSlot);
        }
        else {
            holdingTool = false;
            currentToolName = Tools.HandToolName;
        }

        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            if (currentToolName != transform.GetChild(0).GetChild(i).name) {
                transform.GetChild(0).GetChild(i).GetComponent<Renderer>().enabled = false;
            }
            else {
                transform.GetChild(0).GetChild(i).GetComponent<Renderer>().enabled = true;

                if (itemType.IsTool()) {
                    mouseHitInterval = tools.GetToolHitSpeed(itemTypeInSelectedSlot);
                }
                else
                    mouseHitInterval = Tools.HandHitSpeed;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="button">
    /// 0 - false - left
    /// 1 - true - right
    /// </param>
    private void HandleMouseClick(bool button)
    {
        // Check if holding sth edible and clicking right button
        if (!button) {
            if (inventory.EatSlotItemIfEdible(blockSelectCounter))
                return;

            if (!inventory.IsSlotItemPlaceable(blockSelectCounter))
                return;
        }


        // Hit animation
        if(transform.childCount  > 0) {
            transform.GetChild(0).Find(currentToolName).GetComponent<Animator>().SetTrigger("Hit");
        }


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

                    // Add block hit particle effect
                    GameObject hitEffect = Instantiate(blockHitParticlePrefab, tc.transform);
                    hitEffect.name = bix + " " + biy + " " + biz;
                    hitEffect.transform.position =
                        new Vector3(tc.transform.position.x + bix - .5f, tc.transform.position.y + biy + .6f, tc.transform.position.z + biz - .5f);
                    var ps = hitEffect.GetComponent<ParticleSystem>();
                    var main = ps.main;
                    BlockType blockType = tc.blocks[bix, biy, biz];
                    main.startColor = Block.GetBlockTypeParticlesGradient(blockType);

                    BlockType? block = null;

                    if (!destroyButtonPressed) return;

                    if(holdingTool)
                        block = tc.IncreaseBLockDestroyLevel(bix, biy, biz, currentTool);
                    else
                        block = tc.IncreaseBLockDestroyLevel(bix, biy, biz, null);

                    if (block.HasValue) {
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
                        else if(block == BlockType.CoalOre) {
                            inventory.AddItem(new Item { itemType = ItemType.Coal, amount = 1 });
                        }
                        else if(block == BlockType.IronOre) {
                            inventory.AddItem(new Item { itemType = ItemType.IronOre, amount = 1 });
                        }
                        else if (block == BlockType.DiamondOre) {
                            inventory.AddItem(new Item { itemType = ItemType.Diamond, amount = 1 });
                        }
                        else if(block == BlockType.Leaves) {
                            if(Random.Range(1, 13) == 1) {
                                inventory.AddItem(new Item { itemType = ItemType.Apple, amount = 1 });
                            }
                        }
                        else if (block == BlockType.Furnace) {
                            inventory.AddItem(new Item { itemType = ItemType.Furnace, amount = 1 });
                        }
                        else if(block == BlockType.Torch) {
                            // Remove torch light from chunk
                            for (int i = 0; i < tc.torches.Count; i++) {
                                if (tc.torches[i].name == bix + " " + biy + " " + biz) {
                                    var light = tc.torches[i];
                                    tc.torches.RemoveAt(i);
                                    Destroy(light);
                                }
                            }

                            inventory.AddItem(new Item { itemType = ItemType.Torch, amount = 1 });
                        }
                    }

                    tc.RegenerateMesh();

                    // Regenerate chunks if block on the edge was destroyed

                    // Regenerate Front Chunk
                    if (biz == 1) {
                        TerrainGenerator.chunks[new Vector2Int(chunkPosX, chunkPosZ - 16)].RegenerateMesh();
                    }
                    // Regenerate Back Chunk
                    else if (biz == TerrainChunk.chunkWidth) {
                        TerrainGenerator.chunks[new Vector2Int(chunkPosX, chunkPosZ + 16)].RegenerateMesh();
                    }

                    // Regenerate Right Chunk
                    if (bix == 1) {
                        TerrainGenerator.chunks[new Vector2Int(chunkPosX - 16, chunkPosZ)].RegenerateMesh();
                    }
                    // Regenerate Left Chunk
                    else if (bix == TerrainChunk.chunkWidth) {
                        TerrainGenerator.chunks[new Vector2Int(chunkPosX + 16, chunkPosZ)].RegenerateMesh();
                    }
                }
            }
            // right click
            else {
                if (tc.blocks[bix, biy, biz] != BlockType.Torch && biy <= TerrainChunk.chunkHeight - 2) {  // we can't place blocks above the limit

                    // index of the target block
                    int blockHitX = Mathf.FloorToInt((hitInfo.point + transform.forward * .01f).x) - chunkPosX + 1;
                    int blockHitY = Mathf.FloorToInt((hitInfo.point + transform.forward * .01f).y);
                    int blockHitZ = Mathf.FloorToInt((hitInfo.point + transform.forward * .01f).z) - chunkPosZ + 1;

                    BlockType blockType = inventory.GetSlotItem(blockSelectCounter);

                    if (tc.blocks[blockHitX, blockHitY, blockHitZ] == BlockType.Furnace) {
                        if (blockType == BlockType.IronOre) {
                            inventory.AddItem(ItemType.Iron, 1);
                            return;
                        }
                        else if (blockType == BlockType.Sand) {
                            inventory.AddItem(ItemType.Glass, 1);
                            return;
                        }
                    }

                    if (blockType != BlockType.Air)
                        tc.blocks[bix, biy, biz] = blockType;

                    if (blockType == BlockType.Torch) {
                        GameObject lightGameObject = Instantiate(torchLightPrefab, tc.transform);
                        lightGameObject.name = bix + " " + biy + " " + biz;
                        lightGameObject.transform.position = 
                            new Vector3(tc.transform.position.x + bix - .5f, tc.transform.position.y + biy + .8f, tc.transform.position.z + biz - .5f);
                        tc.torches.Add(lightGameObject);
                    }
                }

                tc.RegenerateMesh();
            }
        }
    }
}
